using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

using AW.Common.Enumerations;
using AW.Common.Interfaces.Repositories;

namespace AW.Infrastructure.Repositories;

public class AzureBlobStorageRepository : IAzureBlobStorageRepository
{
    private readonly string azureStorageConnectionString;
    private readonly IConfiguration _configuration;

    public AzureBlobStorageRepository(IConfiguration configuration)
    {
        this._configuration = configuration;
        this.azureStorageConnectionString = _configuration.GetValue<string>("AzureStorageConnectionString")!;
    }

    public async Task DeleteAsync(AzureContainer container, string blobFilename)
    {
        var containerName = Enum.GetName(typeof(AzureContainer), container)!.ToLower();
        containerName = containerName.Replace("_", "-");
        var blobContainerClient = new BlobContainerClient(this.azureStorageConnectionString, containerName);
        var blobClient = blobContainerClient.GetBlobClient(blobFilename);

        try
        {
            await blobClient.DeleteAsync();
        }
        catch //TODO: es necesario implementar un manejo de excepcion
        {
            //_logger.LogError(ex.Message);
        }
    }

    public async Task<string> UploadAsync(IFormFile file, AzureContainer container, string blobFilename)
    {
        if (file.Length == 0) return null!;

        var containerName = Enum.GetName(typeof(AzureContainer), container)!.ToLower();
        containerName = containerName.Replace("_", "-");

        var blobContainerClient = new BlobContainerClient(this.azureStorageConnectionString, containerName);

        // Get a reference to the blob just uploaded from the API in a container from configuration settings
        if (string.IsNullOrEmpty(blobFilename))
        {
            blobFilename = Guid.NewGuid().ToString();
        }

        var blobClient = blobContainerClient.GetBlobClient(blobFilename);

        var blobHttpHeader = new BlobHttpHeaders { ContentType = file.ContentType };

        // Open a stream for the file we want to upload
        await using (Stream stream = file.OpenReadStream())
        {
            // Upload the file async
            await blobClient.UploadAsync(stream, new BlobUploadOptions { HttpHeaders = blobHttpHeader });
        }

        return blobFilename;
    }
}