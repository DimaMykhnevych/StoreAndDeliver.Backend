using StoreAndDeliver.BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.CargoRequestService
{
    public interface ICargoRequestService
    {
        Task<Dictionary<Guid, List<CargoRequestDto>>> GetCurrentUserRequests(Guid userId, GetRequestDto getRequestDto);
    }
}
