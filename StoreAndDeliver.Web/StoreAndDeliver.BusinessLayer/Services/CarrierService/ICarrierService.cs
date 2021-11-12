using StoreAndDeliver.BusinessLayer.DTOs;
using System;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.CarrierService
{
    public interface ICarrierService
    {
        Task<CarrierDto> GetCarrier(Guid id);
        Task<CarrierDto> GetCarrierByAppUserId(Guid id);
    }
}
