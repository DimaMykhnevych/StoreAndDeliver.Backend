using AutoMapper;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.BusinessLayer.Services.CargoService;
using StoreAndDeliver.BusinessLayer.Services.CarrierService;
using StoreAndDeliver.BusinessLayer.Services.RequestService;
using StoreAndDeliver.DataLayer.Builders.CargoSessionQueryBuilder;
using StoreAndDeliver.DataLayer.Enums;
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
        private readonly ICargoService _cargoService;
        private readonly IMapper _mapper;

        public CargoSessionService(ICargoSessionQueryBuilder queryBuilder,
            IRequestService requestService, ICarrierService carrierService,
            ICargoService cargoService, IMapper mapper)
        {
            _queryBuilder = queryBuilder;
            _requestService = requestService;
            _carrierService = carrierService;
            _cargoService = cargoService;
            _mapper = mapper;
        }

        public async Task<RequestsForIotDto> GetCarrierSessionRequests(Guid userId, GetRequestDto getRequestDto)
        {
            CarrierDto carrier = await _carrierService.GetCarrierByAppUserId(userId);
            var cargoSessions = _queryBuilder
                                .SetBaseCargoSessionInfo()
                                .SetCargoSessionCarrier(carrier.Id)
                                .SetCargoSessionRequestStatus(RequestStatus.InProgress)
                                .SetCargoSessionRequestType(getRequestDto.RequestType)
                                .Build()
                                .ToList();
            var cargoRequests = cargoSessions
                                .Select(c => c.CargoRequest);
            var requests = new List<Dictionary<Guid, List<CargoRequest>>>();
            var cargoRequestsDictionary = cargoRequests
                .GroupBy(cr => cr.RequestId)
                .ToDictionary(k => k.Key, v => v.ToList());
            requests.Add(cargoRequestsDictionary);
            await _requestService.ConvertRequestsValues(requests, getRequestDto.Units, getRequestDto.CurrentLanguage);
            var requestsForIot = new RequestsForIotDto
            {
                CargoRequests = _mapper.Map<IEnumerable<CargoRequestIoTDto>>(cargoRequests)
            };
            var settingsBound = _cargoService
                .GetCargoSettingsBound((_mapper.Map<IEnumerable<CargoRequestDto>>(cargoRequests))
                .Select(c => c.Cargo));
            requestsForIot.SettingsBound = settingsBound.Select(c => new IoTSettingBoundDto { 
                Setting = c.Key,
                MaxValue = c.Value.MaxValue,
                MinValue = c.Value.MinValue
            });
            return requestsForIot;

        }

        public IEnumerable<CargoSessionDto> GetCarrierActiveCargoSessions(Guid carrierId, RequestType requestType)
        {
            var cargoSessions = _queryBuilder
               .SetBaseCargoSessionInfo()
               .SetCargoSessionCarrier(carrierId)
               .SetCargoSessionRequestStatus(RequestStatus.InProgress)
               .SetCargoSessionRequestType(requestType)
               .Build()
               .ToList();
            return _mapper.Map<IEnumerable<CargoSessionDto>>(cargoSessions);
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
