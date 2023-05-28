namespace AuthorizationMicroservice.Requests;

public record GetUserByTokenRequest(
    string Token);