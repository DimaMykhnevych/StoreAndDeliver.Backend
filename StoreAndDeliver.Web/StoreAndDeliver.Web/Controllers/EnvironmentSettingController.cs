using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.BusinessLayer.Services.EnvironmnetSettingService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAndDeliver.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EnvironmentSettingController : ControllerBase
    {
        private readonly IEnvironmnetSettingService _environmnetSettingService;

        public EnvironmentSettingController(IEnvironmnetSettingService environmnetSettingService)
        {
            _environmnetSettingService = environmnetSettingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEnvironmentSettings()
        {
            IEnumerable<EnvironmentSettingDto> settings = 
                await _environmnetSettingService.GetEnvironmentSettings();
            return Ok(settings);
        }
    }
}
