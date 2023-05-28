namespace AuthorizationMicroservice.Requests;

public record LoginUserRequest(
    string Email,
    string Password);