using TWJobs.Core.Data.Contexts;

namespace TWJobs.Core.Config;

public static class DatabaseConfig
{
    public static void RegisterDatabase(this IServiceCollection services)
    {
        services.AddDbContext<TWJobsDbContext>();
    }
}