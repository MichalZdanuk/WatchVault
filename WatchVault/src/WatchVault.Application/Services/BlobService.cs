using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;

namespace WatchVault.Application.Services;
public sealed class BlobService : IBlobService
{
    private readonly BlobContainerClient _containerClient;

    public BlobService(IConfiguration configuration)
    {
        var connectionString = configuration["AzureStorage:ConnectionString"];
        var containerName = configuration["AzureStorage:BlobContainerName"];

        var blobServiceClient = new BlobServiceClient(connectionString);
        _containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        _containerClient.CreateIfNotExists(PublicAccessType.Blob);
    }

    public async Task<Guid> UploadAsync(Stream fileStream, Guid fileId, string contentType)
    {
        var blobClient = _containerClient.GetBlobClient(fileId.ToString());

        await blobClient.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = contentType });

        return fileId;
    }

    public async Task<(Stream Stream, string ContentType)?> GetAsync(Guid fileId)
    {
        var blobClient = _containerClient.GetBlobClient(fileId.ToString());

        if (await blobClient.ExistsAsync())
        {
            var response = await blobClient.DownloadContentAsync();
            var contentType = response.Value.Details.ContentType ?? "application/octet-stream";

            return (response.Value.Content.ToStream(), contentType);
        }

        return null;
    }
}
