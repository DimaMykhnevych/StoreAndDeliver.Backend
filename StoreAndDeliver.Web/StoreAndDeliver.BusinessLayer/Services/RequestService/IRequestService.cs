using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.DataLayer.Enums;
using StoreAndDeliver.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.RequestService
{
    public interface IRequestService
    {
        Task<List<ICollection<CargoRequest>>> GetOptimizedRequestGroups(Guid currentCarrierId, RequestType requestType);
        Task<RequestDto> AddRequest(AddRequestDto addRequestDto);
        Task<decimal> CalculateRequestPrice(AddRequestDto requestAddDto);
    }
}
