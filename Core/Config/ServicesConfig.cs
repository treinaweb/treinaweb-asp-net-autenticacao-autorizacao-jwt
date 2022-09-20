using TWJobs.Api.Auth.Services;
using TWJobs.Api.Jobs.Services;
using TWJobs.Api.Users.Services;

namespace TWJobs.Core.Config;

public static class ServicesConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IJobService, JobService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
    }
}