using MediatR;
using SubSnap.Core.Domain.ValueObjects;

namespace SubSnap.Application.UseCases.Auth.Logout;

//public sealed record LogoutCommand( UserId UserId, string RefreshToken );

public sealed record LogoutCommand(UserId UserId, string RefreshToken) : IRequest;  //x plugin MediatR (vlidazione automatica!)
