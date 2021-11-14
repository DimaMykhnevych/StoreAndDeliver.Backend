using AutoMapper;
using StoreAndDeliver.BusinessLayer.Calculations.Algorithms;
using StoreAndDeliver.BusinessLayer.Constants;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.BusinessLayer.Services.AddressService;
using StoreAndDeliver.BusinessLayer.Services.CargoService;
using StoreAndDeliver.BusinessLayer.Services.CarrierService;
using StoreAndDeliver.BusinessLayer.Services.ConvertionService;
using StoreAndDeliver.BusinessLayer.Services.EmailService;
using StoreAndDeliver.BusinessLayer.Services.StoreService;
using StoreAndDeliver.DataLayer.Builders.RequestQueryBuilder;
using StoreAndDeliver.DataLayer.Enums;
using StoreAndDeliver.DataLayer.Models;
using StoreAndDeliver.DataLayer.Repositories.CargoRequestsRepository;
using StoreAndDeliver.DataLayer.Repositories.CargoSessionRepository;
using StoreAndDeliver.DataLayer.Repositories.RequestRepository;
using StoreAndDeliver.DataLayer.Repositories.UserRepository;
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
        private readonly IStoreService _storeService;
        private readonly ICarrierService _carrierService;
        private readonly IEmailService _emailService;
        private readonly IRequestQueryBuilder _requestQueryBuilder;
        private readonly IRequestAlgorithms _requestAlgorithms;
        private readonly ICargoSessionRepository _cargoSessionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public RequestService(IRequestRepository requestRepository,
            IConvertionService convertionService,
            IAddressService addressService,
            ICargoService cargoService,
            ICargoRequestsRepository cargoRequestsRepository,
            IStoreService storeService,
            ICarrierService carrierService,
            IEmailService emailService,
            IRequestQueryBuilder requestQueryBuilder,
            IRequestAlgorithms requestAlgorithms,
            ICargoSessionRepository cargoSessionRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _requestRepository = requestRepository;
            _convertionService = convertionService;
            _addressService = addressService;
            _cargoService = cargoService;
            _cargoRequestsRepository = cargoRequestsRepository;
            _storeService = storeService;
            _carrierService = carrierService;
            _emailService = emailService;
            _requestQueryBuilder = requestQueryBuilder;
            _requestAlgorithms = requestAlgorithms;
            _cargoSessionRepository = cargoSessionRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<Dictionary<Guid, List<CargoRequest>>>> GetOptimizedRequestGroups
            (Guid currentCarrierId, GetRequestDto getOptimizedRequestDto)
        {
            CarrierDto carrier = await _carrierService.GetCarrierByAppUserId(currentCarrierId);

            //Getting requests with specific type that have not yet been processed
            List<Request> requests = new();
            var baseRequestInfo = _requestQueryBuilder
                .SetBaseRequestInfo()
                .SetRequestType(getOptimizedRequestDto.RequestType);
            var optimizedCargoRequests = new List<List<CargoRequest>>();
            if (getOptimizedRequestDto.RequestType == RequestType.Deliver)
            {
                requests = baseRequestInfo
                    .SetCarryOutBeforeDate(DateTime.Now.AddHours(-5))
                    .SortByDeliverByDate()
                    .Build()
                    .ToList();
                List<CargoRequest> cargoRequests = requests
                    .SelectMany(r => r.CargoRequests)
                    .Where(cr => cr.Status == RequestStatus.Pending).ToList();

                //Requests grouped by all applicable combinations of setting values
                optimizedCargoRequests = OptimizeDeliverRequests(carrier, cargoRequests).ToList();
            }
            else
            {
                requests = baseRequestInfo.SetStoreDates(DateTime.Now, null).Build().ToList();
                List<CargoRequest> cargoRequests = requests
                    .SelectMany(r => r.CargoRequests)
                    .Where(cr => cr.Status == RequestStatus.Pending).ToList();
                optimizedCargoRequests = OptimizeDeliverRequests(carrier, cargoRequests, false).ToList();
            }
            var groupedOptimizedRequests = GroupCargoRequestsByRequests(optimizedCargoRequests);
            await ConvertRequestsValues(groupedOptimizedRequests, 
                getOptimizedRequestDto.Units, 
                getOptimizedRequestDto.CurrentLanguage);
            return groupedOptimizedRequests;
        }

        public async Task<RequestDto> AddRequest(AddRequestDto addRequestDto)
        {
            addRequestDto.Request.RequestDate = DateTime.Now;
            //Adding addresses
            AddressDto addedFromAddress =
                await _addressService.AddAddress(addRequestDto.Request.FromAddress);
            AddressDto addedToAddress = new AddressDto();
            if (addRequestDto.Request.Type != RequestType.Store)
            {
                addedToAddress =
                    await _addressService.AddAddress(addRequestDto.Request.ToAddress);
            }

            //Adding request
            Request request = _mapper.Map<Request>(addRequestDto.Request);
            request.Id = Guid.NewGuid();
            request.AppUserId = addRequestDto.CurrentUserId;
            request.FromAddressId = addedFromAddress.Id;
            request.ToAddressId = addedToAddress.Id == Guid.Empty ? null : addedToAddress.Id;
            request.FromAddress = request.ToAddress = null;
            string fromCurrency = GetCurrencyUnit(addRequestDto.CurrentLanguage);
            request.TotalSum = await _convertionService.ConvertCurrency(fromCurrency, Currency.Usd, request.TotalSum);
            Request addedRequest = await _requestRepository.Insert(request);
            await _requestRepository.Save();

            //Adding cargo
            var cargo = await _cargoService.AddCargoRange(addRequestDto.Cargo, addRequestDto.Units);
            var addedRequestDto = _mapper.Map<RequestDto>(request);

            //Adding CargoRequests
            if (request.Type == RequestType.Store)
            {
                await _storeService.DistrubuteCargoByStores(cargo, addedRequestDto);
            }
            else
            {
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
            }

            return addedRequestDto;
        }

        public async Task<bool> UpdateRequestStatuses(Guid carrierId, UpdateCargoRequestsDto updateModel)
        {
            CarrierDto carrier = await _carrierService.GetCarrierByAppUserId(carrierId);
            bool isCompleted = false;
            try
            {
                foreach (var k in updateModel.RequestGroup.Keys)
                {
                    foreach (var v in updateModel.RequestGroup[k])
                    {
                        _cargoService.ConvertCargoUnits(v.Cargo, updateModel.Units, null);
                        if (v.Status == RequestStatus.InProgress)
                        {
                            double currentVolume = v.Cargo.GetCargoVolume();
                            carrier.CurrentOccupiedVolume += currentVolume;
                            if (carrier.CurrentOccupiedVolume > carrier.MaxCargoVolume)
                            {
                                throw new Exception("Carrier is full");
                            }
                            CargoSession cargoSession = new CargoSession
                            {
                                Id = Guid.NewGuid(),
                                CargoRequestId = v.Id,
                                CarrierId = carrier.Id
                            };
                            await _cargoSessionRepository.Insert(cargoSession);
                            await _cargoSessionRepository.Save();
                        }
                        if (v.Status == RequestStatus.Completed)
                        {
                            isCompleted = true;
                            double currentVolume = v.Cargo.GetCargoVolume();
                            carrier.CurrentOccupiedVolume -= currentVolume;
                            if (carrier.CurrentOccupiedVolume < 0 ||
                                (carrier.CurrentOccupiedVolume > 0 && carrier.CurrentOccupiedVolume < 1))
                                carrier.CurrentOccupiedVolume = 0;
                        }
                        v.Store = null;
                        v.Cargo = null;
                        v.Request = null;
                        await _cargoRequestsRepository.Update(v);
                    }
                    carrier.CargoSeesions = null;
                    carrier.AppUser = null;
                    await _carrierService.UpdateCarrier(carrier);
                }
                if (isCompleted)
                {
                    await SendSuccessfullDeliveryEmail(updateModel);
                }
                return true;
            }
            catch
            {
                return false;
            }
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

        public async Task ConvertRequestsValues
            (List<Dictionary<Guid, List<CargoRequest>>> requestGroups, Units unitsTo, string currentLanguage)
        {
            Units unitsFrom = new Units()
            {
                Weight = WeightUnit.Kilograms,
                Length = LengthUnit.Meters,
                Temperature = TemperatureUnit.Celsius,
                Luminosity = LuminosityUnit.Lux,
                Humidity = HumidityUnit.Percentage
            };
            HashSet<CargoRequest> cargoRequests = new();
            HashSet<Request> requests = new();
            foreach (var group in requestGroups)
            {
                foreach (var keyValue in group)
                {
                    foreach (var value in keyValue.Value)
                    {
                        cargoRequests.Add(value);
                        requests.Add(value.Request);
                    }
                }
            }
            foreach(var cr in cargoRequests)
            {
                _cargoService.ConvertCargoUnits(cr.Cargo, unitsFrom, unitsTo);
                await _cargoService.ConvertCargoSettings(cr.Cargo.CargoSettings, unitsFrom, unitsTo);
            }

            // Converting currency
            //foreach (var r in requests)
            //{
            //    UNCOMMENT to real currency convertion
            //    if (currentLanguage != Languages.ENGLISH)
            //    {
            //        string toCurrency = GetCurrencyUnit(currentLanguage);
            //        decimal price = await _convertionService.ConvertCurrency(Currency.Usd, toCurrency, keyValue.Value[0].Request.TotalSum);
            //        keyValue.Value[0].Request.TotalSum = price;
            //    }
            //}
        }

        private async Task SendSuccessfullDeliveryEmail(UpdateCargoRequestsDto updateModel)
        {
            var cargoRequests = updateModel.RequestGroup.SelectMany(c => c.Value).ToList();
            foreach(var r in cargoRequests)
            {
                if(r.Request == null)
                {
                    r.Request = _requestQueryBuilder
                        .SetBaseRequestInfo()
                        .SetRequestId(r.RequestId)
                        .Build().FirstOrDefault();
                }
                var requestUser = r.Request.AppUser;
                if(requestUser == null)
                {
                    r.Request.AppUser = await _userRepository.Get(r.Request.AppUserId);
                }
                r.Cargo = r.Request.CargoRequests.FirstOrDefault(cr => cr.Id == r.Id).Cargo;
            }
            var requestsGroupedByUser = cargoRequests
                .GroupBy(cr => cr.Request.AppUser)
                .ToDictionary(k => k.Key, k => k.ToList());
            await _emailService.SendSuccessfullDeliveryEmail(requestsGroupedByUser, updateModel.Language);
        }

        private static List<Dictionary<Guid, List<CargoRequest>>> GroupCargoRequestsByRequests
            (List<List<CargoRequest>> cargoRequests)
        {
            List<Dictionary<Guid, List<CargoRequest>>> result = new();
            foreach(var cr in cargoRequests)
            {
                Dictionary<Guid, List<CargoRequest>> groupedRequests = cr.GroupBy(c => c.RequestId)
                    .ToDictionary(g => g.Key, g => g.ToList());
                result.Add(groupedRequests);
            }
            return result;
        }

        private List<List<CargoRequest>> OptimizeDeliverRequests(CarrierDto carrier, 
            List<CargoRequest> cargoRequests, bool optimizeRoute = true)
        {
            var optimizedRequests = _requestAlgorithms.GetOptimizedRequests(cargoRequests);
            var requestsOptimizedByRoute = optimizedRequests;
            if (optimizeRoute)
            {
                requestsOptimizedByRoute = _requestAlgorithms.GetOptimizedRouteForCargoRequestGroups(optimizedRequests);
            }
            // Check carrier capacity.
            double currentCapacity = carrier.CurrentOccupiedVolume;
            var requestsToProceedByCurrentCarrier = new List<List<CargoRequest>>();
            foreach (var optimizedRequest in requestsOptimizedByRoute)
            {
                foreach(var request in optimizedRequest)
                {
                    currentCapacity += request.Cargo.GetCargoVolume();
                }
                if(currentCapacity < carrier.MaxCargoVolume)
                {
                    requestsToProceedByCurrentCarrier.Add(optimizedRequest);
                }
                currentCapacity = 0;
            }

            return requestsToProceedByCurrentCarrier;
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
