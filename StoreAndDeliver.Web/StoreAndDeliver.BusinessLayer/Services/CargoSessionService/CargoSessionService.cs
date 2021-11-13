using AutoMapper;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.BusinessLayer.Services.CarrierService;
using StoreAndDeliver.BusinessLayer.Services.RequestService;
using StoreAndDeliver.DataLayer.Builders.CargoSessionQueryBuilder;
using StoreAndDeliver.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.CargoSessionService
{
    public class CargoSessionService : ICargoSessionService
    {
        private readonly ICargoSessionQueryBuilder _queryBuilder;
        private readonly IRequestService _requestService;
        private readonly ICarrierService _carrierService;
        private readonly IMapper _mapper;

        public CargoSessionService(ICargoSessionQueryBuilder queryBuilder,
            IRequestService requestService, ICarrierService carrierService,
            IMapper mapper)
        {
            _queryBuilder = queryBuilder;
            _requestService = requestService;
            _carrierService = carrierService;
            _mapper = mapper;
        }

        public async Task<Dictionary<Guid, List<CargoRequestDto>>> GetCarrierRequests
            (Guid userId, GetRequestDto getRequestDto)
        {
            var requests = new List<Dictionary<Guid, List<CargoRequest>>>();
            CarrierDto carrier = await _carrierService.GetCarrierByAppUserId(userId);
            var cargoRequests = _queryBuilder
                .SetBaseCargoSessionInfo()
                .SetCargoSessionCarrier(carrier.Id)
                .SetCargoSessionRequestStatus(getRequestDto.Status)
                .SetCargoSessionRequestType(getRequestDto.RequestType)
                .Build()
                .ToList()
                .Select(c => c.CargoRequest);
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
