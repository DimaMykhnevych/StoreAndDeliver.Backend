using AutoMapper;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.DataLayer.Models;
using StoreAndDeliver.DataLayer.Repositories.AddressRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.AddressService
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        public AddressService(IAddressRepository addressRepository, IMapper mapper)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AddressDto>> GetAddresses()
        {
            IEnumerable<Address> addresses = await _addressRepository.GetAll();
            return _mapper.Map<IEnumerable<AddressDto>>(addresses);
        }

        public async Task<AddressDto> AddAddress(AddressDto addressDto)
        {
            Address address = _mapper.Map<Address>(addressDto);
            address.Id = new System.Guid();
            var addedAddress = await _addressRepository.Insert(address);
            await _addressRepository.Save();
            return _mapper.Map<AddressDto>(addedAddress);
        }
    }
}
