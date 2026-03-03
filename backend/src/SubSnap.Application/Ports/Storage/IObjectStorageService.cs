namespace SubSnap.Application.Ports.Storage;

/*
 * x media(foto+video) dell'user, salvati su Hetzner Object.
 *see IObjectStorageService.cs HetznerObjectStorageService.cs UserMedia.cs xxxhandler.cs
 */
public interface IObjectStorageService
{
    Task<string> UploadAsync(
        Stream file,
        string fileName,
        string contentType,
        CancellationToken ct);

    Task DeleteAsync(string key, CancellationToken ct);
}