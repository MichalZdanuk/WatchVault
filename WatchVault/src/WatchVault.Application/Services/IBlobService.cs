namespace WatchVault.Application.Services;
public interface IBlobService
{
    Task<Guid> UploadAsync(Stream fileStream, Guid fileId, string contentType);
    Task<(Stream Stream, string ContentType)?> GetAsync(Guid fileId);
}
