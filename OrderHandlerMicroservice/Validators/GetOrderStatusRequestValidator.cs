using FluentValidation;
using OrderHandlerMicroservice.Requests;

namespace OrderHandlerMicroservice.Validators;

public class GetOrderStatusRequestValidator : AbstractValidator<GetOrderStatusRequest>
{
    public GetOrderStatusRequestValidator()
    {
        RuleFor(x => x.OrderId)
            .GreaterThan(0);
    }
}