using Microsoft.AspNetCore.Mvc;
using StoreAndDeliver.BusinessLayer.Constants;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.BusinessLayer.Services.CargoSessionService;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StoreAndDeliver.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoSessionController : ControllerBase
    {
        private readonly ICargoSessionService _cargoSessionService;

        public CargoSessionController(ICargoSessionService cargoSessionService)
        {
            _cargoSessionService = cargoSessionService;
        }

        [HttpPost]
        [Route("filteredCarrierRequests")]
        public async Task<IActionResult> FilteredCarrierRequests([FromBody] GetRequestDto getOptimizedRequestDto)
        {
            Guid carrierId = new Guid(User.FindFirstValue(AuthorizationConstants.ID));
            var result = await _cargoSessionService.GetCarrierRequests(carrierId, getOptimizedRequestDto);
            return Ok(result);
        }
    }
}
