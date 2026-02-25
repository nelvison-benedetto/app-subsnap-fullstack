namespace SubSnap.API.Contracts.Users;

public sealed class RegisterUserRequest
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}
