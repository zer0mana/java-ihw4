namespace OrderHandlerMicroservice.Requests;

public record AddDishesRequestItem(    
    string Name,
    string Description,
    decimal Price,
    int Quantity);
public record AddDishesRequest(
    string Token,
    AddDishesRequestItem[] Dishes);