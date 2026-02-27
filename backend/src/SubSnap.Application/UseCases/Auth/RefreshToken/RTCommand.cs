using MediatR;

namespace SubSnap.Application.UseCases.Auth.RefreshToken;

public sealed record RTCommand( string RefreshToken ) : IRequest<RTResult>;
