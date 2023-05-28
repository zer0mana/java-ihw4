using AuthorizationMicroservice.Requests;
using FluentValidation;

namespace AuthorizationMicroservice.Validators;

public class LoginUserRequestValidator : AbstractValidator<LoginUserRequest>
{
    public LoginUserRequestValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .MaximumLength(30);
        RuleFor(x => x.Password)
            .NotNull()
            .MaximumLength(20)
            .MinimumLength(8);
    }
}