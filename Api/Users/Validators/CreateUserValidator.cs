using FluentValidation;
using TWJobs.Api.Users.Dtos;

namespace TWJobs.Api.Users.Validators;

public class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserValidator()
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