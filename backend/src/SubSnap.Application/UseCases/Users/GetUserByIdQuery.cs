using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubSnap.Application.UseCases.Users;

//queries solo lettura 
public sealed record GetUserByIdQuery(int UserId);