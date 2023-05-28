namespace OrderHandlerMicroservice.Requests;

public record CreateOrderRequestItem(
    int DishId,
    int Quantity);

public record CreateOrderRequest(
    string Token,
    CreateOrderRequestItem[] Dishes,
    string SpecialRequests);