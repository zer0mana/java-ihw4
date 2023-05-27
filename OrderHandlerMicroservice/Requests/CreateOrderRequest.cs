namespace OrderHandlerMicroservice.Requests;

public record CreateOrderRequest(
    int[] DishIds,
    string SpecialRequests);