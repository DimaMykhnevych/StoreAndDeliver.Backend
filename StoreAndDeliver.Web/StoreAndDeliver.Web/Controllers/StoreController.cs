using Microsoft.AspNetCore.Mvc;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.BusinessLayer.Services.StoreService;
using System;
using System.Threading.Tasks;

namespace StoreAndDeliver.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStores()
        {
            var stores = await _storeService.GetStores();
            return Ok(stores);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStore([FromBody] AddStoreDto storeDto)
        {
            StoreDto addedStore = await _storeService.CreateStore(storeDto);
            return Ok(addedStore);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStore(Guid id)
        {
            return Ok(await _storeService.DeleteStore(id));
        }
    }
}
