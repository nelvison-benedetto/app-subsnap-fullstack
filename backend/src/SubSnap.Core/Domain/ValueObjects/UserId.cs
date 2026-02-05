using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubSnap.Core.Domain.ValueObjects;

public readonly struct UserId  //è readonly struct
{
    public int Value { get; }
    public UserId(int value)
    {
        if (value <= 0)
            throw new ArgumentException("UserId must be positive");

        Value = value;
    }
}
