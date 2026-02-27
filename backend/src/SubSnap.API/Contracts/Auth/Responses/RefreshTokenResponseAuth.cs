namespace SubSnap.API.Contracts.Auth.Responses;

public sealed class RefreshTokenResponseAuth
{
    public required string AccessToken { get; init; }
    public required string RefreshToken { get; init; }
}
