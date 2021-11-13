using StoreAndDeliver.BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.CargoSessionService
{
    public interface ICargoSessionService
    {
        Task<Dictionary<Guid, List<CargoRequestDto>>> GetCarrierRequests(Guid userId, GetRequestDto getRequestDto);
    }
}
