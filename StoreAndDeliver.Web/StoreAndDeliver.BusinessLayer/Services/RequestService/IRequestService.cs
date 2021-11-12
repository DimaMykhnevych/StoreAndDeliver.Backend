using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.RequestService
{
    public interface IRequestService
    {
        Task<List<Dictionary<Guid, List<CargoRequest>>>> GetOptimizedRequestGroups(Guid currentCarrierId, GetOptimizedRequestDto getOptimizedRequestDto);
        Task<RequestDto> AddRequest(AddRequestDto addRequestDto);
        Task<decimal> CalculateRequestPrice(AddRequestDto requestAddDto);
    }
}
