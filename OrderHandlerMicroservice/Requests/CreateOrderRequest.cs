namespace OrderHandlerMicroservice.Requests;

public record CreateOrderRequestItem(
    int DishId,
    int Quantity);

public record CreateOrderRequest(
    int UserId,
    CreateOrderRequestItem[] Dishes,
    string SpecialRequests);