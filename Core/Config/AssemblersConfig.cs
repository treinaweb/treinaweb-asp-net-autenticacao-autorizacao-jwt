using TWJobs.Api.Common.Assemblers;
using TWJobs.Api.Jobs.Assemblers;
using TWJobs.Api.Jobs.Dtos;

namespace TWJobs.Core.Config;

public static class AssemblersConfig
{
    public static void RegisterAssemblers(this IServiceCollection services)
    {
        services.AddScoped<IAssembler<JobSummaryResponse>, JobSummaryAssembler>();
        services.AddScoped<IAssembler<JobDetailResponse>, JobDetailAssembler>();
        services.AddScoped<IPagedAssembler<JobSummaryResponse>, JobSummaryPagedAssembler>();
    }
}