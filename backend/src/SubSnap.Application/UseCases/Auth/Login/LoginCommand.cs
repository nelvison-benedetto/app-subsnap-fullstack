using MediatR;
using SubSnap.Core.Domain.ValueObjects;

namespace SubSnap.Application.UseCases.Auth.Login;

//public sealed record LoginCommand(Email Email, string PlainPassword);
public sealed record LoginCommand(Email Email, string Password) : IRequest<LoginResult>;  //x plugin MediatR (validazione automatica!)


