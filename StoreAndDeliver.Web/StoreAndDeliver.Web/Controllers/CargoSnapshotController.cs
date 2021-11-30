using Microsoft.AspNetCore.Mvc;
using StoreAndDeliver.BusinessLayer.Constants;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.BusinessLayer.Services.CargoSnapshotService;
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

        public CargoSnapshotController(ICargoSnapshotService cargoSnapshotService)
        {
            _cargoSnapshotService = cargoSnapshotService;
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
    }
}
