using AutoMapper;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.DataLayer.Models;
using StoreAndDeliver.DataLayer.Repositories.AddressRepository;
using StoreAndDeliver.DataLayer.Repositories.CityRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.AddressService
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public AddressService(IAddressRepository addressRepository,
            ICityRepository cityRepository,
            IMapper mapper)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
            _cityRepository = cityRepository;
        }

        public async Task<IEnumerable<AddressDto>> GetAddresses()
        {
            IEnumerable<Address> addresses = await _addressRepository.GetAll();
            return _mapper.Map<IEnumerable<AddressDto>>(addresses);
        }

        public async Task<AddressDto> AddAddress(AddressDto addressDto)
        {
            Address address = _mapper.Map<Address>(addressDto);
            Address existingAddress = await _addressRepository.Get(address.Id);
            if (existingAddress != null)
            {
                return _mapper.Map<AddressDto>(existingAddress);
            }
            address.Id = Guid.NewGuid();
            await GetAddressCoordinates(address);
            var addedAddress = await _addressRepository.Insert(address);
            await _addressRepository.Save();
            return _mapper.Map<AddressDto>(addedAddress);
        }

        public async Task<bool> DeleteAddress(Guid id)
        {
            Address address = await _addressRepository.Get(id);
            if(address == null)
            {
                return false;
            }
            _addressRepository.Delete(address);
            await _addressRepository.Save();
            return true;
        }

        private async Task GetAddressCoordinates(Address address)
        {
            City city = await _cityRepository.GetCityByAddress(address);
            address.Latitude = city.Latitude;
            address.Longtitude = city.Longtitude;
        }
    }
}
