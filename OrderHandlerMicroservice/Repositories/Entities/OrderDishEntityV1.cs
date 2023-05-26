namespace OrderHandlerMicroservice.Repositories.Entities;

public record OrderDishEntityV1(
    int Id,
    int OrderId,
    int DishId,
    int Quantity,
    decimal Price);