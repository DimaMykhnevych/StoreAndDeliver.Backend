using Microsoft.AspNetCore.Mvc;
using StoreAndDeliver.BusinessLayer.Constants;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.BusinessLayer.Services.RequestService;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StoreAndDeliver.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpGet]
        [Route("optimizedRequests")]
        public async Task<IActionResult> GetOptimizedRequestGroups()
        {
            var result = await _requestService.GetOptimizedRequestGroups
                (new Guid("0044add8-b3ea-414b-8b72-1312f91dafd8"), DataLayer.Enums.RequestType.Deliver);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddRequest([FromBody] AddRequestDto addRequestDto)
        {
            addRequestDto.CurrentUserId = new Guid(User.FindFirstValue(AuthorizationConstants.ID));
            RequestDto result = await _requestService.AddRequest(addRequestDto);
            return Ok(result);
        }

        [HttpPost]
        [Route("price")]
        public async Task<IActionResult> GetRequestPrice([FromBody] AddRequestDto addRequestDto)
        {
            //UNCOMMENT for calulating real price

            addRequestDto.CurrentUserId = new Guid(User.FindFirstValue(AuthorizationConstants.ID));
            //decimal price = await _requestService.CalculateRequestPrice(addRequestDto);
            return Ok(10);
        }
    }
}
