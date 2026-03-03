using SubSnap.Core.Domain.Common;
using SubSnap.Core.Domain.ValueObjects;

namespace SubSnap.Core.Domain.Entities;

//see IObjectStorageService.cs HetznerObjectStorageService.cs UserMedia.cs xxxhandler.cs
public class UserMedia : AggregateRoot
{
    public Guid Id { get; private set; }
    public UserId UserId { get; private set; }

    public string FileName { get; private set; }
    public string Url { get; private set; }
    public string ContentType { get; private set; }
    public long Size { get; private set; }

    private UserMedia() { }

    public UserMedia(
        UserId userId,
        string fileName,
        string url,
        string contentType,
        long size)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        FileName = fileName;
        Url = url;
        ContentType = contentType;
        Size = size;
    }
}