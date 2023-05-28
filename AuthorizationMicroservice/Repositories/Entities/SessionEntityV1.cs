namespace AuthorizationMicroservice.Repositories.Entities;

public record SessionEntityV1
{
    public SessionEntityV1()
    {
    }
    
    public SessionEntityV1(
        int id,
        int userId,
        string sessionToken,
        DateTimeOffset expiresAt)
    {
        Id = id;
        UserId = userId;
        SessionToken = sessionToken;
        ExpiresAt = expiresAt;
    }

    public int Id { get; set; }
    public int UserId { get; set; }
    public string SessionToken { get; set; }
    public DateTimeOffset ExpiresAt { get; set; }
}