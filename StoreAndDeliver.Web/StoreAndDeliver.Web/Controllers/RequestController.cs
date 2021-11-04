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

        [HttpPost]
        [Route("price")]
        public async Task<IActionResult> GetRequestPrice([FromBody] AddRequestDto addRequestDto)
        {
            //Don't forget to set request date
            addRequestDto.CurrentUserId = new Guid(User.FindFirstValue(AuthorizationConstants.ID));
            decimal price = await _requestService.CalculateRequestPrice(addRequestDto);
            return Ok(price);
        }
    }
}
