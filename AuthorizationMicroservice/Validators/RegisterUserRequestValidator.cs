using AuthorizationMicroservice.Requests;
using FluentValidation;

namespace AuthorizationMicroservice.Validators;

public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserRequestValidator()
    {
        RuleFor(x => x.Username)
            .NotNull()
            .MaximumLength(10)
            .MinimumLength(5);
        RuleFor(x => x.Email)
            .EmailAddress()
            .MaximumLength(30);
        RuleFor(x => x.Password)
            .NotNull()
            .MaximumLength(20)
            .MinimumLength(8);
    }
}