namespace SubSnap.Core.Domain.ValueObjects;

public readonly struct SharedLinkId
{
    public Guid Value { get; }
    public SharedLinkId(Guid value)
    {
        if (value == Guid.Empty) throw new ArgumentException("Invalid SharedLinkId");
        Value = value;
    }

    public static SharedLinkId New() => new SharedLinkId(Guid.NewGuid());  //generi nuovo Id!! COSI IL DOMAIN E' INDIPENDENTE DAL DB, e non devi fare un SaveChanges() solo per ottenere l'id delle nuova row!!

    public override string ToString() => Value.ToString();

    //equals
    public bool Equals(SharedLinkId other) => Value.Equals(other.Value);
    public override bool Equals(object? obj) => obj is SharedLinkId other && Equals(other);
    public static bool operator ==(SharedLinkId left, SharedLinkId right) => left.Equals(right);
    public static bool operator !=(SharedLinkId left, SharedLinkId right) => !(left == right);

}
