using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubSnap.Core.Domain.ValueObjects;

public readonly struct Email  //è readonly struct
{
    public string Value { get; }
    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email is required");

        if (!value.Contains('@'))
            throw new ArgumentException("Invalid email");

        Value = value;
    }

    public override string ToString() => Value;
}
