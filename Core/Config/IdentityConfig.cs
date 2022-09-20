using Microsoft.AspNetCore.Identity;
using TWJobs.Core.Data.Contexts;

namespace TWJobs.Core.Config;

public static class IdentityConfig
{
    public static IServiceCollection RegisterIdentity(this IServiceCollection services)
    {
        services.AddIdentity<IdentityUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.User.RequireUniqueEmail = true;
        })
            .AddEntityFrameworkStores<TWJobsDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}