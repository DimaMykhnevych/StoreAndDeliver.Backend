using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.BusinessLayer.Services.CargoService;
using StoreAndDeliver.DataLayer.Models;
using System.Threading.Tasks;

namespace StoreAndDeliver.Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CargoController : ControllerBase
    {
        private readonly ICargoService _cargoService;

        public CargoController(ICargoService cargoService)
        {
            _cargoService = cargoService;
        }

        [HttpPost]
        [Route("getCargoSettingRecommendations")]
        [Authorize(Roles = Role.User)]
        public async Task<IActionResult> GetCargoSettingRecommendations(
            [FromBody] GetRecommendedSettingsDto getRecommendedSettings)
        {
            var result = await _cargoService.GetRecommendationCargoSettings(getRecommendedSettings);
            return Ok(result);
        }
    }
}
