using StoreAndDeliver.BusinessLayer.DTOs;
using System;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.AzureBlobService
{
    public interface IAzureBlobService
    {
        Task<GetCargoPhotosDto> GetCargoPhotos(Guid cargoRequestId);
    }
}
