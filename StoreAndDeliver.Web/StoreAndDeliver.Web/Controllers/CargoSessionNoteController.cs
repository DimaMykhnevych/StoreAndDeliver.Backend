using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.BusinessLayer.Services.CargoSessionNoteService;
using System;
using System.Threading.Tasks;

namespace StoreAndDeliver.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CargoSessionNoteController : ControllerBase
    {
        private readonly ICargoSessionNoteService _cargoSessionNoteService;

        public CargoSessionNoteController(ICargoSessionNoteService cargoSessionNoteService)
        {
            _cargoSessionNoteService = cargoSessionNoteService;
        }

        [HttpGet("getNotesByCargoRequestId/{cargoRequestId}")]
        public async Task<IActionResult> GetNotesByCargoRequestId(Guid cargoRequestId)
        {
            var notes = await _cargoSessionNoteService.GetNotesByCargoRequestId(cargoRequestId);
            return Ok(notes);
        }

        [HttpPost("addNoteToCargoRequestInSession")]
        public async Task<IActionResult> AddCargoSessionNote([FromBody] AddCargoSessionNoteDto addCargoSessionNoteDto)
        {
            var addedNote = await _cargoSessionNoteService.AddCargoSessionNote(addCargoSessionNoteDto);
            return Ok(addedNote);
        }
    }
}
