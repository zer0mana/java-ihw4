namespace OrderHandlerMicroservice.Requests;

public record CreateOrderRequest(
    long[] DishIds,
    string SpecialRequests);