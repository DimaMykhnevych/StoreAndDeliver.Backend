﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
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

        public async Task<GetCargoPhotosDto> GetCargoPhotos(Guid cargoRequestId)
        {
            _logger.LogInformation($"Start getting photos for cargo request with id: {cargoRequestId}");
            BlobServiceClient blobServiceClient = new BlobServiceClient(_azureStorageAccountOptions.ConnectionString);
            var container = blobServiceClient.GetBlobContainerClient(AzureStorageConstants.CargoPhotosContainerName);
            BlobSasBuilder sasBuilder = new BlobSasBuilder()
            {
                ExpiresOn = DateTime.UtcNow.AddMinutes(5),
                Resource = "b"
            };
            sasBuilder.SetPermissions(BlobAccountSasPermissions.Read);
            var results = new List<string>();
            await foreach (var blob in container.GetBlobsAsync(prefix: cargoRequestId.ToString()))
            {
                BlobClient blobClient = container.GetBlobClient(blob.Name);
                results.Add(blobClient.GenerateSasUri(sasBuilder).ToString());
            }
            return new GetCargoPhotosDto { PhotoUrls = results };
        }
    }
}
