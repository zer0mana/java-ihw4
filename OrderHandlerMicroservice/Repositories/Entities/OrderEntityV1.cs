namespace OrderHandlerMicroservice.Repositories.Entities;

public record OrderEntityV1(
    int Id,
    int UserId,
    string Status,
    string SpecialRequests,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt);