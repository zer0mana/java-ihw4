namespace OrderHandlerMicroservice.Responses;

public record GetOrderStatusResponse(
    int Id,
    string OrderStatus);