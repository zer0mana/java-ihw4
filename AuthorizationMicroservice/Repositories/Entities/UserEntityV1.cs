namespace AuthorizationMicroservice.Repositories.Entities;

public record UserEntityV1
{
    public UserEntityV1()
    {
        
    }

    public UserEntityV1(
        int id,
        string username,
        string email,
        string passwordHash,
        string role,
        DateTimeOffset createdAt,
        DateTimeOffset updatedAt)
    {
        Id = id;
        Username = username;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}