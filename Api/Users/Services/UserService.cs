using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using TWJobs.Api.Users.Dtos;
using TWJobs.Api.Users.Mappers;

namespace TWJobs.Api.Users.Services;

public class UserService : IUserService
{
    private readonly IUserMapper _userMapper;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IValidator<CreateUserRequest> _createUserValidator;

    public UserService(
        IUserMapper userMapper,
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IValidator<CreateUserRequest> createUserValidator)
    {
        _userMapper = userMapper;
        _userManager = userManager;
        _roleManager = roleManager;
        _createUserValidator = createUserValidator;
    }

    public async Task<UserResponse> CreateAsync(CreateUserRequest request)
    {
        await _createUserValidator.ValidateAndThrowAsync(request);

        var user = _userMapper.ToModel(request);
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            throw new ValidationException(result.Errors.Select(x => new ValidationFailure("global", x.Description)));
        }

        var role = await _roleManager.FindByNameAsync("Admin");
        if (role is null)
        {
            role = new IdentityRole("Admin");
            await _roleManager.CreateAsync(role);
        }
        await _userManager.AddToRoleAsync(user, role.Name);

        return _userMapper.ToResponse(user);
    }
}