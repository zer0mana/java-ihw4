namespace AuthorizationMicroservice.Options;

public class JwtConfig
{
    public static readonly string Key = "gayaubxcfuspyzziaxkiwhaaftwvgosxtepppelgqmfdigmwxt";
    public static readonly string Issuer = "http://localhost:5002";
    public static readonly string Audience = "http://localhost:5001";
    public static readonly int SessionDuration = 5;
}