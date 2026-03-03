using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubSnap.Application.UseCases.Users.GetUsersWithSubscriptions;

public sealed record GUSCommand() : IRequest<List<GUSResult>>;