namespace OrderHandlerMicroservice.Requests;

public record UpdateDishRequest(
    int DishId,
    int ChangeCount);