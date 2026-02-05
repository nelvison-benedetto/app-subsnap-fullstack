using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubSnap.Core.Domain.Exceptions;

public abstract class NotFoundException : Exception
{
    protected NotFoundException(string message) : base(message) { }
}

// esempio specifico per User
public sealed class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(int userId)
        : base($"User with id {userId} was not found.") { }
}