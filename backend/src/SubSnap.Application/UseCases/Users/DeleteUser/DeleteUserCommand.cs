using MediatR;
using SubSnap.Core.Domain.ValueObjects;

namespace SubSnap.Application.UseCases.Users.DeleteUser;

public sealed record DeleteUserCommand( UserId userId ) : IRequest;