using StoreAndDeliver.BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.AzureBlobService
{
    public interface IAzureBlobService
    {
        Task<IEnumerable<CargoPhotoDto>> GetCargoPhotos(Guid cargoRequestId);
    }
}
