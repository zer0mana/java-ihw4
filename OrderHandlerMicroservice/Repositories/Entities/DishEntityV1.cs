namespace OrderHandlerMicroservice.Repositories.Entities;

public record DishEntityV1
{
    public DishEntityV1()
    {
        
    }

    public DishEntityV1(
        int Id,
        string Name,
        string Description,
        decimal Price,
        int Quantity)
    {
        this.Id = Id;
        this.Name = Name;
        this.Description = Description;
        this.Price = Price;
        this.Quantity = Quantity;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}