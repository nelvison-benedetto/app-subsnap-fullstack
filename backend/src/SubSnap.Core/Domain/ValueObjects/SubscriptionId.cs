using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubSnap.Core.Domain.ValueObjects;

public readonly struct SubscriptionId
{
    public int Value { get; }
    public SubscriptionId(int value)
    {
        if (value <= 0) throw new ArgumentException("Invalid SubscriptionId");
        Value = value;
    }
}
