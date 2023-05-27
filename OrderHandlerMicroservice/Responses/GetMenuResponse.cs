namespace OrderHandlerMicroservice.Responses;

public record MenuItem(
    int Id,
    string Name,
    string Description,
    decimal Price);

public record GetMenuResponse(
    MenuItem[] MenuItems);