using AutoMapper;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.BusinessLayer.Services.RequestService;
using StoreAndDeliver.DataLayer.Builders.CargoRequestQueryBuilder;
using StoreAndDeliver.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.CargoRequestService
{
    public class CargoRequestService : ICargoRequestService
    {
        private readonly ICargoRequestQueryBuilder _queryBuilder;
        private readonly IRequestService _requestService;
        private readonly IMapper _mapper;

        public CargoRequestService(
            IRequestService requestService, 
            ICargoRequestQueryBuilder queryBuilder, 
            IMapper mapper)
        {
            _queryBuilder = queryBuilder;
            _mapper = mapper;
            _requestService = requestService;
        }

        public async Task<Dictionary<Guid, List<CargoRequestDto>>> GetCurrentUserRequests(
            Guid userId,
            GetRequestDto getRequestDto)
        {
            var requests = new List<Dictionary<Guid, List<CargoRequest>>>();
            var cargoRequests = _queryBuilder
                .SetBaseCargoRequestInfo()
                .SetCargoRequestUser(userId)
                .SetCargoRequestType(getRequestDto.RequestType)
                .SetCargoRequestStatus(getRequestDto.Status)
                .Build()
                .ToList();
            var cargoRequestsDictionary = cargoRequests
                .GroupBy(cr => cr.RequestId)
                .ToDictionary(k => k.Key, v => v.ToList());
            requests.Add(cargoRequestsDictionary);
            await _requestService.ConvertRequestsValues(requests, getRequestDto.Units, getRequestDto.CurrentLanguage);
            var result = _mapper.Map<Dictionary<Guid, List<CargoRequestDto>>>(requests[0]);
            return result;
        }
    }
}
