using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreAndDeliver.BusinessLayer.Constants;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.BusinessLayer.Services.AzureBlobService;
using StoreAndDeliver.BusinessLayer.Services.CargoRequestService;
using StoreAndDeliver.DataLayer.Models;
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
        private readonly IAzureBlobService _azureBlobService;

        public CargoRequestController(ICargoRequestService cargoRequestService, IAzureBlobService azureBlobService)
        {
            _cargoRequestService = cargoRequestService;
            _azureBlobService = azureBlobService;
        }

        [HttpPost]
        [Route("getUserCargoRequests")]
        public async Task<IActionResult> GetUserCargoRequests([FromBody] GetRequestDto getRequestDto)
        {
            var currentUserId = new Guid(User.FindFirstValue(AuthorizationConstants.ID));
            var requests = await _cargoRequestService.GetCurrentUserRequests(currentUserId, getRequestDto);
            return Ok(requests);
        }

        [HttpGet]
        [Route("getCargoPhotos/{cargoRequestId}")]
        public async Task<IActionResult> GetCargoPhotos(Guid cargoRequestId)
        {
            var result = await _azureBlobService.GetCargoPhotos(cargoRequestId);
            return Ok(result);
        }

        [HttpPost]
        [Route("uploadCargoPhoto/{cargoRequestId}")]
        [Authorize(Roles = Role.Carrier)]
        public async Task<IActionResult> UploadCargoPhoto(Guid cargoRequestId, [FromForm] IFormFile file)
        {
            if(file == null)
            {
                file = HttpContext.Request.Form.Files[0];
            }
            var result = await _azureBlobService.UploadCargoPhoto(cargoRequestId, file);
            return Ok(result);
        }
    }
}
