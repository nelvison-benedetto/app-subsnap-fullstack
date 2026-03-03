using Amazon.S3;
using Amazon.S3.Model;
using SubSnap.Application.Ports.Storage;

namespace SubSnap.Infrastructure.External.Storage;

/*
see IObjectStorageService.cs HetznerObjectStorageService.cs UserMedia.cs xxxhandler.cs
 */

public class HetznerObjectStorageService : IObjectStorageService
{
    private readonly IAmazonS3 _s3;

    public HetznerObjectStorageService(IAmazonS3 s3)
    {
        _s3 = s3;
    }

    public async Task<string> UploadAsync(
        Stream file,
        string fileName,
        string contentType,
        CancellationToken ct)
    {
        var key = $"users/{Guid.NewGuid()}-{fileName}";

        var request = new PutObjectRequest
        {
            BucketName = "subsnap-media",
            Key = key,
            InputStream = file,
            ContentType = contentType
        };

        await _s3.PutObjectAsync(request, ct);

        return $"https://fsn1.your-endpoint/{key}";
    }
}