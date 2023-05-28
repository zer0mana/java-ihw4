namespace OrderHandlerMicroservice.Repositories.Entities;

public record OrderDishEntityV1
{
    public OrderDishEntityV1()
    {
    }
    
    public OrderDishEntityV1(
        int Id,
        int OrderId,
        int DishId,
        int Quantity,
        decimal Price)
    {
        this.Id = Id;
        this.OrderId = OrderId;
        this.DishId = DishId;
        this.Quantity = Quantity;
        this.Price = Price;
    }

    public int Id;
    public int OrderId;
    public int DishId;
    public int Quantity;
    public decimal Price;
}