//namespace SubSnap.Core.DTOs.External.Responses.Users;

namespace SubSnap.Application.Contracts.Users;


//quello che esponi al world (xk non esponi mai domain o entity)
public sealed class UserResponse
{
    public Guid Id { get; init; }
    public string Email { get; init; } = null!;
}
