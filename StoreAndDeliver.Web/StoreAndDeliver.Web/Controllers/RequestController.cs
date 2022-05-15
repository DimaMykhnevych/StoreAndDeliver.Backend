using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreAndDeliver.BusinessLayer.Calculations.Statistics;
using StoreAndDeliver.BusinessLayer.Constants;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.BusinessLayer.Services.RequestService;
using StoreAndDeliver.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StoreAndDeliver.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;
        private readonly IRequestStatistics _requestStatistics;

        public RequestController(IRequestService requestService, IRequestStatistics requestStatistics)
        {
            _requestService = requestService;
            _requestStatistics = requestStatistics;
        }

        [HttpGet]
        [Route("countriesStatistics")]
        [Authorize(Roles = Role.CompanyAdmin)]
        public IActionResult GetRequestsByCountriesStatistics()
        {
            var result = _requestStatistics.GetRequestsByCountriesStatistics();
            return Ok(result);
        }

        [HttpGet]
        [Route("citiesStatistics")]
        [Authorize(Roles = Role.CompanyAdmin)]
        public IActionResult GetRequestsByCitiesStatistics()
        {
            var result = _requestStatistics.GetRequestsByCitiesStatistics();
            return Ok(result);
        }

        [HttpPost]
        [Route("optimizedRequests")]
        [Authorize(Roles = Role.Carrier)]
        public async Task<IActionResult> GetOptimizedRequestGroups([FromBody] GetRequestDto getOptimizedRequestDto)
        {
            //TODO uncomment in _requestService.ConvertRequestsValues for real currency converting
            Guid carrierId = new Guid(User.FindFirstValue(AuthorizationConstants.ID));
            var result = await _requestService.GetOptimizedRequestGroups
                (carrierId, getOptimizedRequestDto);
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
            decimal price = await _requestService.CalculateRequestPrice(addRequestDto);
            return Ok(price);
        }

        [HttpPut]
        [Route("updateRequestStautses")]
        public async Task<IActionResult> UpdateRequestStatuses
            ([FromBody] UpdateCargoRequestsDto requests)
        {
            var carrierId = new Guid(User.FindFirstValue(AuthorizationConstants.ID));
            var result = await _requestService.UpdateRequestStatuses(carrierId, requests);
            return Ok(result);
        }
    }
}
