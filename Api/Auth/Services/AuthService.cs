using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using TWJobs.Api.Auth.Dtos;
using TWJobs.Api.Auth.Mappers;
using TWJobs.Core.Exceptions;

namespace TWJobs.Api.Auth.Services;

public class AuthService : IAuthService
{
    private readonly string _jwtSigningKey;
    private readonly int _jwtExpirationInMinutes;

    private readonly IAuthMapper _authMapper;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IValidator<LoginRequest> _loginValidator;
    private readonly IValidator<RegisterRequest> _registerValidator;

    public AuthService(
        IAuthMapper authMapper,
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IValidator<LoginRequest> loginValidator,
        IValidator<RegisterRequest> registerValidator,
        IConfiguration configuration)
    {
        _authMapper = authMapper;
        _userManager = userManager;
        _roleManager = roleManager;
        _loginValidator = loginValidator;
        _registerValidator = registerValidator;

        _jwtSigningKey = configuration.GetValue<string>("Jwt:SigningKey");
        _jwtExpirationInMinutes = configuration.GetValue<int>("Jwt:ExpirationInMinutes");
    }

    public async Task<LoginResponse> Login(LoginRequest request)
    {
        await _loginValidator.ValidateAndThrowAsync(request);

        var user = await _userManager.FindByNameAsync(request.Username);
        if (user is null)
        {
            throw new BadCredentialsException();
        }
        if (!await _userManager.CheckPasswordAsync(user, request.Password))
        {
            throw new BadCredentialsException();
        }
        var key = Encoding.ASCII.GetBytes(_jwtSigningKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            ),
            IssuedAt = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddSeconds(_jwtExpirationInMinutes),
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.UserName)
            })
        };
        var roles = await _userManager.GetRolesAsync(user);
        roles.ToList()
            .ForEach(role => tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, role)));
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new LoginResponse
        {
            Token = tokenHandler.WriteToken(token)
        };
    }

    public async Task<RegisterResponse> Register(RegisterRequest request)
    {
        await _registerValidator.ValidateAndThrowAsync(request);

        var user = _authMapper.ToModel(request);
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            throw new ValidationException(result.Errors.Select(x => new FluentValidation.Results.ValidationFailure("global", x.Description)));
        }

        var role = await _roleManager.FindByNameAsync("User");
        if (role is null)
        {
            role = new IdentityRole("User");
            await _roleManager.CreateAsync(role);
        }
        await _userManager.AddToRoleAsync(user, role.Name);

        return _authMapper.ToRegisterResponse(user);
    }
}
