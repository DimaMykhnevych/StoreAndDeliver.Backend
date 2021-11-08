using AutoMapper;
using StoreAndDeliver.BusinessLayer.Constants;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.BusinessLayer.Services.AddressService;
using StoreAndDeliver.BusinessLayer.Services.CargoService;
using StoreAndDeliver.BusinessLayer.Services.ConvertionService;
using StoreAndDeliver.DataLayer.Enums;
using StoreAndDeliver.DataLayer.Models;
using StoreAndDeliver.DataLayer.Repositories.CargoRequestsRepository;
using StoreAndDeliver.DataLayer.Repositories.RequestRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.RequestService
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _requestRepository;
        private readonly ICargoRequestsRepository _cargoRequestsRepository;
        private readonly IConvertionService _convertionService;
        private readonly IAddressService _addressService;
        private readonly ICargoService _cargoService;
        private readonly IMapper _mapper;

        public RequestService(IRequestRepository requestRepository,
            ICargoRequestsRepository cargoRequestsRepository,
            IConvertionService convertionService,
            IAddressService addressService,
            ICargoService cargoService,
            IMapper mapper)
        {
            _requestRepository = requestRepository;
            _cargoRequestsRepository = cargoRequestsRepository;
            _convertionService = convertionService;
            _addressService = addressService;
            _cargoService = cargoService;
            _mapper = mapper;
        }

        public async Task<RequestDto> AddRequest(AddRequestDto addRequestDto)
        {
            addRequestDto.Request.RequestDate = DateTime.Now;
            //Adding addresses
            AddressDto addedFromAddress =
                await _addressService.AddAddress(addRequestDto.Request.FromAddress);
            AddressDto addedToAddress =
                await _addressService.AddAddress(addRequestDto.Request.ToAddress);

            //Adding request
            Request request = _mapper.Map<Request>(addRequestDto.Request);
            request.Id = Guid.NewGuid();
            request.AppUserId = addRequestDto.CurrentUserId;
            request.FromAddressId = addedFromAddress.Id;
            request.ToAddressId = addedToAddress.Id;
            request.FromAddress = request.ToAddress = null;
            string fromCurrency = GetCurrencyUnit(addRequestDto.CurrentLanguage);
            request.TotalSum = await _convertionService.ConvertCurrency(fromCurrency, Currency.Usd, request.TotalSum);
            Request addedRequest = await _requestRepository.Insert(request);
            await _requestRepository.Save();

            //Adding cargo
            var cargo = await _cargoService.AddCargoRange(addRequestDto.Cargo, addRequestDto.Units);

            //Adding CargoRequests
            foreach (var c in cargo)
            {
                var cargoRequest = new CargoRequest()
                {
                    Id = Guid.NewGuid(),
                    CargoId = c.Id,
                    RequestId = addedRequest.Id
                };
                await _cargoRequestsRepository.Insert(cargoRequest);
                await _cargoRequestsRepository.Save();
            }
            return _mapper.Map<RequestDto>(request);
        }

        public async Task<decimal> CalculateRequestPrice(AddRequestDto requestAddDto)
        {
            decimal bonus = await CalculateBonusForUser(requestAddDto.CurrentUserId);
            double totalWeight = requestAddDto.Cargo.Sum(x => x.Weight * x.Amount);
            double totalWeigthInKg = _convertionService.ConvertWeigth(requestAddDto.Units.Weight, WeightUnit.Kilograms, totalWeight);
            int settingsAmount = requestAddDto.Cargo.SelectMany(c => c.Settings).Count();
            decimal sum = (settingsAmount * 10 + (decimal)(totalWeigthInKg * 3)) / bonus;

            if (requestAddDto.CurrentLanguage != Languages.ENGLISH)
            {
                string toCurrency = GetCurrencyUnit(requestAddDto.CurrentLanguage);
                return await _convertionService.ConvertCurrency(Currency.Usd, toCurrency, sum);
            }

            return sum;
        }

        private async Task<decimal> CalculateBonusForUser(Guid id)
        {
            IEnumerable<Request> userRequests = await _requestRepository.GetUserRequests(id);
            int requestsAmount = userRequests.Count();
            switch (requestsAmount)
            {
                case < 5:
                    return 1M;
                case < 10:
                    return 1.2M;
                case >= 10:
                    return 1.4M;
            }
        }

        private static string GetCurrencyUnit(string currentLanguage)
        {

            return currentLanguage switch
            {
                Languages.RUSSIAN => Currency.Rub,
                Languages.UKRAINAN => Currency.Uah,
                _ => Currency.Usd
            };
        }
    }
}
