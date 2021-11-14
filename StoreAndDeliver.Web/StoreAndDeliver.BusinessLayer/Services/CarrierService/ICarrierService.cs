using StoreAndDeliver.BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.CarrierService
{
    public interface ICarrierService
    {
        Task<CarrierDto> GetCarrier(Guid id);
        Task<CarrierDto> GetCarrierByAppUserId(Guid id);
        Task<IEnumerable<CarrierDto>> GetCarriers();
        Task<CarrierDto> AddCarrier(AddCarrierDto addCarrierDto);
        Task UpdateCarrier(CarrierDto carrierDto);
        Task UpdateCarrierWithUser(UpdateCarrierDto updateCarrierDto);
        Task<bool> DeleteCarrier(Guid id);
    }
}
