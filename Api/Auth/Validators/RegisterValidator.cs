using FluentValidation;
using TWJobs.Api.Auth.Dtos;

namespace TWJobs.Api.Auth.Validators;

public class RegisterValidator : AbstractValidator<RegisterRequest>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .OverridePropertyName("username");

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6)
            .OverridePropertyName("password");

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .OverridePropertyName("email");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .OverridePropertyName("phoneNumber");
    }
}