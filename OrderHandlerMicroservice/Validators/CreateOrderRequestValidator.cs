using FluentValidation;
using OrderHandlerMicroservice.Requests;

namespace OrderHandlerMicroservice.Validators;

public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderRequestValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0);
        RuleFor(x => x.SpecialRequests)
            .MaximumLength(100);
        RuleFor(x => x.Dishes)
            .NotEmpty();
    }
}

public class CreateOrderRequestItemValidator : AbstractValidator<CreateOrderRequestItem>
{
    public CreateOrderRequestItemValidator()
    {
        RuleFor(x => x.DishId)
            .GreaterThan(0);
        RuleFor(x => x.Quantity)
            .GreaterThan(0);
    }
}