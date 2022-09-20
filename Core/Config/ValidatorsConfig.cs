using FluentValidation;
using TWJobs.Api.Auth.Dtos;
using TWJobs.Api.Auth.Validators;
using TWJobs.Api.Jobs.Dtos;
using TWJobs.Api.Jobs.Validators;
using TWJobs.Api.Users.Dtos;
using TWJobs.Api.Users.Validators;

namespace TWJobs.Core.Config;

public static class ValidatorsConfig
{
    public static void RegisterValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<JobRequest>, JobRequestValidator>();
        services.AddScoped<IValidator<RegisterRequest>, RegisterValidator>();
        services.AddScoped<IValidator<CreateUserRequest>, CreateUserValidator>();
    }
}