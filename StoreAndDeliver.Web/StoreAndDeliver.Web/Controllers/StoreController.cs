using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreAndDeliver.BusinessLayer.Calculations.Algorithms.StoreAlgorithms;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.BusinessLayer.Services.StoreService;
using StoreAndDeliver.DataLayer.Models;
using System;
using System.Threading.Tasks;

namespace StoreAndDeliver.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;
        private readonly IStoreAlgorithms _storeAlgorithms;

        public StoreController(IStoreService storeService, IStoreAlgorithms storeAlgorithms)
        {
            _storeService = storeService;
            _storeAlgorithms = storeAlgorithms;
        }

        [HttpGet]
        public async Task<IActionResult> GetStores()
        {
            var stores = await _storeService.GetStores();
            return Ok(stores);
        }
        [HttpGet]
        [Authorize(Roles = Role.CompanyAdmin)]
        [Route("getOptimalStoreLocation")]
        public async Task<IActionResult> GetOptimalStoreLocation()
        {
            var location = await _storeAlgorithms.GetOptimalStoreLocations();
            return Ok(location);
        }

        [HttpPost]
        [Authorize(Roles = Role.CompanyAdmin)]
        public async Task<IActionResult> CreateStore([FromBody] AddStoreDto storeDto)
        {
            StoreDto addedStore = await _storeService.CreateStore(storeDto);
            return Ok(addedStore);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Role.CompanyAdmin)]
        public async Task<IActionResult> DeleteStore(Guid id)
        {
            return Ok(await _storeService.DeleteStore(id));
        }
    }
}
