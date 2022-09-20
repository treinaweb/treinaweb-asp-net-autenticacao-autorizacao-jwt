using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using TWJobs.Api.Auth.Dtos;
using TWJobs.Api.Auth.Mappers;

namespace TWJobs.Api.Auth.Services;

public class AuthService : IAuthService
{
    private readonly IAuthMapper _authMapper;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IValidator<RegisterRequest> _registerValidator;

    public AuthService(
        IAuthMapper authMapper,
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IValidator<RegisterRequest> registerValidator)
    {
        _authMapper = authMapper;
        _userManager = userManager;
        _roleManager = roleManager;
        _registerValidator = registerValidator;
    }

    public async Task<RegisterResponse> Register(RegisterRequest request)
    {
        await _registerValidator.ValidateAndThrowAsync(request);

        var user = _authMapper.ToModel(request);
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            throw new ValidationException(result.Errors.Select(x => new ValidationFailure("global", x.Description)));
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
