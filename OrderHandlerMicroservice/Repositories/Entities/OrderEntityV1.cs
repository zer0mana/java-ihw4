namespace OrderHandlerMicroservice.Repositories.Entities;

public record OrderEntityV1
{
    public OrderEntityV1()
    {
        
    }

    public OrderEntityV1(
        int Id,
        int UserId,
        string Status,
        string SpecialRequests,
        DateTimeOffset CreatedAt,
        DateTimeOffset UpdatedAt)
    {
        this.Id = Id;
        this.UserId = UserId;
        this.Status = Status;
        this.SpecialRequests = SpecialRequests;
        this.CreatedAt = CreatedAt;
        this.UpdatedAt = UpdatedAt;
    }
    
    public int Id;
    public int UserId;
    public string Status;
    public string SpecialRequests;
    public DateTimeOffset CreatedAt;
    public DateTimeOffset UpdatedAt;
}