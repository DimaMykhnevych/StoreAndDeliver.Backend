using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreAndDeliver.BusinessLayer.Constants;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.BusinessLayer.Services.CarrierService;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StoreAndDeliver.Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CarrierController : ControllerBase
    {
        private readonly ICarrierService _carrierService;

        public CarrierController(ICarrierService carrierService)
        {
            _carrierService = carrierService;
        }

        [HttpGet("getCurrentLoggedInCarrier")]
        public async Task<IActionResult> GetCurrentLoggedInCarrier()
        {
            var carrierId = new Guid(User.FindFirstValue(AuthorizationConstants.ID));
            CarrierDto carrier = await _carrierService.GetCarrierByAppUserId(carrierId);
            return Ok(carrier);
        }
    }
}
