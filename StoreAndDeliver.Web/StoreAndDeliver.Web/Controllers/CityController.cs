using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.BusinessLayer.Services.CityService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAndDeliver.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        public IActionResult GetCities([FromQuery] SearchCityDto searchCityDto)
        {
            IEnumerable<CityDto> cities =  _cityService.GetCities(searchCityDto);
            return Ok(cities);
        }

        [HttpPost]
        public async Task<IActionResult> AddCity([FromBody] CityDto city)
        {
            CityDto addedCity = await _cityService.AddCity(city);
            return Ok(addedCity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(Guid id)
        {
            bool result = await _cityService.DeleteCity(id);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
