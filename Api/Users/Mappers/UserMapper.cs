using Microsoft.AspNetCore.Identity;
using TWJobs.Api.Users.Dtos;

namespace TWJobs.Api.Users.Mappers;

public class UserMapper : IUserMapper
{
    public IdentityUser ToModel(CreateUserRequest request)
    {
        return new IdentityUser
        {
            UserName = request.Username,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
        };
    }

    public UserResponse ToResponse(IdentityUser user)
    {
        return new UserResponse
        {
            Id = user.Id,
            Username = user.UserName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
        };
    }
}