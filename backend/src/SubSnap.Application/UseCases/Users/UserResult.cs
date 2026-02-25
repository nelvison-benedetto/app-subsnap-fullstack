using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubSnap.Application.UseCases.Users;

//questi sono usati dai Services, poi mappati in Responses
public sealed record UserResult(
    Guid Id,
    string Email
);


