namespace AuthorizationMicroservice.Requests;

public record RegisterUserRequest(
    string Username,
    string Email,
    string Password,
    string Role);