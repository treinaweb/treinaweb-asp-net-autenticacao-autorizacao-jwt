using Microsoft.AspNetCore.Identity;
using TWJobs.Api.Auth.Dtos;

namespace TWJobs.Api.Auth.Services;

public interface IAuthService
{
    Task<RegisterResponse> Register(RegisterRequest request);
}