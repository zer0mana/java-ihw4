using AuthorizationMicroservice.Requests;
using FluentValidation;

namespace AuthorizationMicroservice.Validators;

public class GetUserByTokenRequestValidator : AbstractValidator<GetUserByTokenRequest>
{
    public GetUserByTokenRequestValidator()
    {
        RuleFor(x => x.Token)
            .NotNull();
    }
}