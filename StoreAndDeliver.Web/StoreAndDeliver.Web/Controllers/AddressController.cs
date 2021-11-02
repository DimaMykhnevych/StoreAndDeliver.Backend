using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.BusinessLayer.Services.AddressService;
using StoreAndDeliver.DataLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAndDeliver.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAddresses()
        {
            IEnumerable<AddressDto> addresses = await _addressService.GetAddresses();
            return Ok(addresses);
        }

        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> AddAdress([FromBody] AddressDto address)
        {
            AddressDto addedAddress = await _addressService.AddAddress(address);
            return Ok(addedAddress);
        }
    }
}
