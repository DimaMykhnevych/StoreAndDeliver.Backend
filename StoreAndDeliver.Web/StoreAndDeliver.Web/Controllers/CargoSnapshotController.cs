using Microsoft.AspNetCore.Mvc;
using StoreAndDeliver.BusinessLayer.Constants;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.BusinessLayer.Services.CargoSnapshotService;
using StoreAndDeliver.BusinessLayer.Services.EmailService;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StoreAndDeliver.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoSnapshotController : ControllerBase
    {
        private readonly ICargoSnapshotService _cargoSnapshotService;
        private readonly IEmailService _emailService;

        public CargoSnapshotController(ICargoSnapshotService cargoSnapshotService, IEmailService emailService)
        {
            _cargoSnapshotService = cargoSnapshotService;
            _emailService = emailService;
        }

        [HttpGet("getUserCargoSnapshots")]
        public async Task<IActionResult> GetUserCargoSnapsots([FromQuery] GetCargoSnapshotDto getCargoSnapshotDto)
        {
            var userId = new Guid(User.FindFirstValue(AuthorizationConstants.ID));
            var result = await _cargoSnapshotService.GetUserCargoSnapshots(userId, getCargoSnapshotDto);
            return Ok(result);
        }

        [HttpGet("getSnapshotsByCargoRequestId")]
        public async Task<IActionResult> GetCargoSnapshotsByCargoRequestId([FromQuery] GetCargoSnapshotDto getCargoSnapshotDto)
        {
            var result = await _cargoSnapshotService.GetCargoSnapshotsByCargoRequestId(getCargoSnapshotDto);
            return Ok(result);
        }

        [HttpGet("getSnapshotsByCargoSessionId/{sessionId}")]
        public async Task<IActionResult> GetCargoSnapshotsByCargoSessionId(Guid sessionId)
        {
            var result = await _cargoSnapshotService.GetCargoSnapshotsByCargoSessionId(sessionId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddCargoSnapshot([FromBody] AddCargoSnapshotDto addCargoSnapshotDto)
        {
            var result = await _cargoSnapshotService.AddCargoSnapshot(addCargoSnapshotDto);
            return Ok(result);
        }

        [HttpPost("addCurrentCarrierSessionSnapshots")]
        public async Task<IActionResult> AddCurrentCarrierCargoSnapshots(
            [FromBody] AddCurrentCarrierCargoSnapshotDto cargoSnapshotDto)
        {
            var userId = new Guid(User.FindFirstValue(AuthorizationConstants.ID));
            var result = await _cargoSnapshotService.AddCurrentCarrierCargoSnapshots(userId, cargoSnapshotDto);
            return Ok(result);
        }

        [HttpPost("sendMotionDetectedEmail/{language}")]
        public async Task<IActionResult> SendMotionDetectedEmail(string language)
        {
            await _emailService.SendMotionDetectedEmail(User.Identity.Name, language);
            return Ok();
        }
    }
}
