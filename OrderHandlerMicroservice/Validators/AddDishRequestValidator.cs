using FluentValidation;
using OrderHandlerMicroservice.Requests;

namespace OrderHandlerMicroservice.Validators;

public class AddDishRequestValidator : AbstractValidator<AddDishesRequest>
{
    public AddDishRequestValidator()
    {
        RuleFor(x => x.Token)
            .NotNull();
        RuleFor(x => x.Dishes)
            .NotEmpty();
    }
}

public class AddDishesRequestItemValidator : AbstractValidator<AddDishesRequestItem>
{
    public AddDishesRequestItemValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .MaximumLength(30);
        RuleFor(x => x.Description)
            .NotNull()
            .MaximumLength(50);
        RuleFor(x => x.Price)
            .GreaterThan(0);
        RuleFor(x => x.Quantity)
            .GreaterThan(0);
    }
}