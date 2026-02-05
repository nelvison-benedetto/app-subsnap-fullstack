using SubSnap.Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubSnap.Core.Domain.Entities;

public class SharedLink
{
    public SharedLinkId Id { get; }
    public string Link { get; }
    public DateTime? ExpireAt { get; }
    public int Views { get; private set; }
    public SharedLink(
        SharedLinkId id,
        string link,
        DateTime? expireAt,
        int views)
    {
        Id = id;
        Link = link;
        ExpireAt = expireAt;
        Views = views;
    }
}
