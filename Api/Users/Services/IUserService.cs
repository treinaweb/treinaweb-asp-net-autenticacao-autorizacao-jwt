using TWJobs.Api.Users.Dtos;

namespace TWJobs.Api.Users.Services;

public interface IUserService
{
    Task<UserResponse> CreateAsync(CreateUserRequest request);
}