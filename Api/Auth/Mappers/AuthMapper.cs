using Microsoft.AspNetCore.Identity;
using TWJobs.Api.Auth.Dtos;

namespace TWJobs.Api.Auth.Mappers;

public class AuthMapper : IAuthMapper
{
    public IdentityUser ToModel(RegisterRequest request)
    {
        return new IdentityUser
        {
            UserName = request.Username,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
        };
    }

    public RegisterResponse ToRegisterResponse(IdentityUser user)
    {
        return new RegisterResponse
        {
            Id = user.Id,
            Username = user.UserName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
        };
    }
}
