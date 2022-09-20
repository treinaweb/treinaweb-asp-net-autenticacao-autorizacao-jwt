using FluentValidation;
using TWJobs.Api.Auth.Dtos;

namespace TWJobs.Api.Auth.Validators;

public class LoginValidator : AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(x => x.Username).NotEmpty().OverridePropertyName("username");
        RuleFor(x => x.Password).NotEmpty().OverridePropertyName("password");
    }
}