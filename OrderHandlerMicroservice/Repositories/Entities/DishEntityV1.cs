namespace OrderHandlerMicroservice.Repositories.Entities;

public record DishEntityV1(
    int Id,
    string Name,
    string Description,
    decimal Price,
    int Quantity);