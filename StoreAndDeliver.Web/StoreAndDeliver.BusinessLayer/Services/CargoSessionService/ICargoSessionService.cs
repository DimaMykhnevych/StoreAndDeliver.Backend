using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.DataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.CargoSessionService
{
    public interface ICargoSessionService
    {
        Task<Dictionary<Guid, List<CargoRequestDto>>> GetCarrierRequests(Guid userId, GetRequestDto getRequestDto);
        Task<RequestsForIotDto> GetCarrierSessionRequests(Guid userId, GetRequestDto getRequestDto);
        IEnumerable<CargoSessionDto> GetCarrierActiveCargoSessions(Guid carrierId, RequestType requestType);
    }
}
