using Microsoft.AspNetCore.Identity;
using TWJobs.Api.Users.Dtos;

namespace TWJobs.Api.Users.Mappers;

public interface IUserMapper
{
    UserResponse ToResponse(IdentityUser user);
    IdentityUser ToModel(CreateUserRequest request);
}