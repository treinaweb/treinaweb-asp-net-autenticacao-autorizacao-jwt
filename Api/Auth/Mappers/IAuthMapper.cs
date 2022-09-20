using Microsoft.AspNetCore.Identity;
using TWJobs.Api.Auth.Dtos;

namespace TWJobs.Api.Auth.Mappers;

public interface IAuthMapper
{
    IdentityUser ToModel(RegisterRequest request);
    RegisterResponse ToRegisterResponse(IdentityUser user);
}