using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreAndDeliver.BusinessLayer.Constants;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.BusinessLayer.Services.CargoRequestService;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StoreAndDeliver.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CargoRequestController : ControllerBase
    {
        private readonly ICargoRequestService _cargoRequestService;

        public CargoRequestController(ICargoRequestService cargoRequestService)
        {
            _cargoRequestService = cargoRequestService;
        }

        [HttpPost]
        [Route("getUserCargoRequests")]
        public async Task<IActionResult> GetUserCargoRequests([FromBody] GetRequestDto getRequestDto)
        {
            var currentUserId = new Guid(User.FindFirstValue(AuthorizationConstants.ID));
            var requests = await _cargoRequestService.GetCurrentUserRequests(currentUserId, getRequestDto);
            return Ok(requests);
        }
    }
}
