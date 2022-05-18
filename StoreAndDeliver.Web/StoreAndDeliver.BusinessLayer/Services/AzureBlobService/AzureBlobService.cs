using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StoreAndDeliver.BusinessLayer.Constants;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.BusinessLayer.Options;

namespace StoreAndDeliver.BusinessLayer.Services.AzureBlobService
{
    public class AzureBlobService : IAzureBlobService
    {
        private readonly AzureStorageAccountOptions _azureStorageAccountOptions;
        private readonly ILogger _logger;

        public AzureBlobService(IOptions<AzureStorageAccountOptions> options, ILoggerFactory loggerFactory)
        {
            _azureStorageAccountOptions = options.Value;
            _logger = loggerFactory?.CreateLogger("Carrier Service");
        }

        public async Task<IEnumerable<CargoPhotoDto>> GetCargoPhotos(Guid cargoRequestId)
        {
            _logger.LogInformation($"Start getting photos for cargo request with id: {cargoRequestId}");
            BlobServiceClient blobServiceClient = new(_azureStorageAccountOptions.ConnectionString);
            var container = blobServiceClient.GetBlobContainerClient(AzureStorageConstants.CargoPhotosContainerName);
            BlobSasBuilder sasBuilder = new()
            {
                ExpiresOn = DateTime.UtcNow.AddMinutes(5),
                Resource = "b"
            };
            sasBuilder.SetPermissions(BlobAccountSasPermissions.Read);
            List<CargoPhotoDto> results = new();
            await foreach (var blob in container.GetBlobsAsync(prefix: cargoRequestId.ToString()))
            {
                BlobClient blobClient = container.GetBlobClient(blob.Name);
                results.Add(new()
                {
                    PhotoUrl = blobClient.GenerateSasUri(sasBuilder).ToString(),
                    Date = blobClient.GetProperties().Value.CreatedOn.LocalDateTime,
                });
            }
            return results.OrderBy(p => p.Date);
        }

        public async Task<bool> UploadCargoPhoto(Guid cargoRequestId, IFormFile photo)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(_azureStorageAccountOptions.ConnectionString);
            var container = blobServiceClient.GetBlobContainerClient(AzureStorageConstants.CargoPhotosContainerName);

            if (!photo.ContentType.Contains("image") || photo.Length <= 0)
            {
                return false;
            }
            try
            {
                await container.UploadBlobAsync($"{cargoRequestId}/{photo.FileName}", photo.OpenReadStream());
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error during uploadin cargo photo to blob: {ex.Message}");
                return false;
            }
        }
    }
}
