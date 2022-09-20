using TWJobs.Api.Auth.Mappers;
using TWJobs.Api.Jobs.Mappers;
using TWJobs.Api.Users.Mappers;

namespace TWJobs.Core.Config;

public static class MappersConfig
{
    public static void RegisterMappers(this IServiceCollection services)
    {
        services.AddScoped<IJobMapper, JobMapper>();
        services.AddScoped<IAuthMapper, AuthMapper>();
        services.AddScoped<IUserMapper, UserMapper>();
    }
}