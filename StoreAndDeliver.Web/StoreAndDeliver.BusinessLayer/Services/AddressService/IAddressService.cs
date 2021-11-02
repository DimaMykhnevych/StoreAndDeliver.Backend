using StoreAndDeliver.BusinessLayer.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.AddressService
{
    public interface IAddressService
    {
        Task<IEnumerable<AddressDto>> GetAddresses();
        Task<AddressDto> AddAddress(AddressDto addressDto);
    }
}
