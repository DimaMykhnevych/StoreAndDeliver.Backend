using AutoMapper;
using GeoCoordinatePortable;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.BusinessLayer.Services.AddressService;
using StoreAndDeliver.DataLayer.Models;
using StoreAndDeliver.DataLayer.Repositories.CargoRequestsRepository;
using StoreAndDeliver.DataLayer.Repositories.RequestRepository;
using StoreAndDeliver.DataLayer.Repositories.StoreRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.StoreService
{
    public class StoreService : IStoreService
    {
        private readonly ICargoRequestsRepository _cargoRequestsRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly IRequestRepository _requestRepository;
        private readonly IAddressService _addressService;
        private readonly IMapper _mapper;

        public StoreService(
            ICargoRequestsRepository cargoRequestsRepository, 
            IStoreRepository storeRepository,
            IRequestRepository requestRepository,
            IAddressService addressService,
            IMapper mapper)
        {
            _cargoRequestsRepository = cargoRequestsRepository;
            _storeRepository = storeRepository;
            _requestRepository = requestRepository;
            _addressService = addressService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StoreDto>> GetStores()
        {
            IEnumerable<Store> stores = await _storeRepository.GetStoresWithAddress();
            return _mapper.Map<IEnumerable<StoreDto>>(stores);
        }

        public async Task<StoreDto> CreateStore(AddStoreDto storeDto)
        {
            AddressDto storeAddress = await _addressService.AddAddress(storeDto.Address);
            storeDto.AddressId = storeAddress.Id;
            storeDto.Address = null;
            Store store = _mapper.Map<Store>(storeDto);
            store.Id = Guid.NewGuid();
            await _storeRepository.Insert(store);
            await _storeRepository.Save();
            return _mapper.Map<StoreDto>(store);
        }

        public async Task<bool> DeleteStore(Guid id)
        {
            Store store = await _storeRepository.Get(id);
            if(store == null)
            {
                return false;
            }
            Guid addressId = store.AddressId;
            _storeRepository.Delete(store);
            await _storeRepository.Save();
            await _addressService.DeleteAddress(addressId);
            return true;
        }

        public async Task<bool> DistrubuteCargoByStores(IEnumerable<CargoDto> cargo, RequestDto request)
        {
            await DeleteOutdatedCargoFromStore();
            var totalVolume = GetTotalCargoVolume(cargo);
            var aproppriateStores = (await _storeRepository.GetStoresWithAddress())
                .Where(s => (s.MaxCargoVolume - s.CurrentOccupiedVolume) >= totalVolume);
            if (!aproppriateStores.Any()) return false;
            Store optimalStore = GetOptimalStore(request, aproppriateStores);
            foreach (var c in cargo)
            {
                var cargoRequest = new CargoRequest()
                {
                    Id = Guid.NewGuid(),
                    CargoId = c.Id,
                    RequestId = request.Id,
                    StoreId = optimalStore.Id
                };
                await _cargoRequestsRepository.Insert(cargoRequest);
                await _cargoRequestsRepository.Save();
                optimalStore.CurrentOccupiedVolume += GetCargoVolume(c);
                await _storeRepository.UpdateStore(optimalStore);
            }
            return true;
        }

        public async Task DeleteOutdatedCargoFromStore()
        {
            var outdatedRequests = await _requestRepository.GetOutdatedStoreRequests();

            foreach(var cargoRequest in outdatedRequests)
            {
                foreach(var r in cargoRequest.CargoRequests)
                {
                    if (r.Store != null && r.Store.CurrentOccupiedVolume > 0)
                    {
                        r.Store.CurrentOccupiedVolume -= r.Cargo.GetCargoVolume();
                        await _storeRepository.UpdateStore(r.Store);
                    }
                }
            }
        }

        private static Store GetOptimalStore(RequestDto request, IEnumerable<Store> stores)
        {
            var requestFromLocation = new GeoCoordinate(request.FromAddress.Latitude, request.FromAddress.Longtitude);
            double minimumDistance = Double.MaxValue;
            Guid optimalStoreId = new Guid();
            foreach(var store in stores)
            {
                var storeLocation = new GeoCoordinate(store.Address.Latitude, store.Address.Longtitude);
                var distance = requestFromLocation.GetDistanceTo(storeLocation);
                if (distance < minimumDistance)
                {
                    minimumDistance = distance;
                    optimalStoreId = store.Id;
                }
            }
            return stores.FirstOrDefault(s => s.Id == optimalStoreId);
        }

        private static double GetCargoVolume(CargoDto c)
        {
            return (c.Height * c.Length * c.Width) * c.Amount;
        }

        private static double GetTotalCargoVolume(IEnumerable<CargoDto> cargo)
        {
            double volume = 0;
            foreach(var c in cargo)
            {
                volume += (c.Height * c.Length * c.Width) * c.Amount;
            }
            return volume;
        }
    }
}
