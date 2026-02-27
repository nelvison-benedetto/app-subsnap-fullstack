using MediatR;
using SubSnap.Core.Domain.ValueObjects;

namespace SubSnap.Application.UseCases.Auth.Login;

//public sealed record LoginCommand(Email Email, string PlainPassword);
public sealed record LoginCommand(Email Email, string PlainPassword) : IRequest<LoginResult>;  //x plugin MediatR (vlidazione automatica!)


