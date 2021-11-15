using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using StoreAndDeliver.BusinessLayer.Constants;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.BusinessLayer.Exceptions;
using StoreAndDeliver.BusinessLayer.Services.CarrierService;
using StoreAndDeliver.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StoreAndDeliver.Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CarrierController : ControllerBase
    {
        private readonly ICarrierService _carrierService;

        public CarrierController(ICarrierService carrierService)
        {
            _carrierService = carrierService;
        }

        [HttpGet]
        [Authorize(Roles = Role.CompanyAdmin)]
        public async Task<IActionResult> GetCarriers()
        {
            IEnumerable<CarrierDto> carriers = await _carrierService.GetCarriers();
            return Ok(carriers);
        }

        [HttpGet("getCurrentLoggedInCarrier")]
        public async Task<IActionResult> GetCurrentLoggedInCarrier()
        {
            var carrierId = new Guid(User.FindFirstValue(AuthorizationConstants.ID));
            CarrierDto carrier = await _carrierService.GetCarrierByAppUserId(carrierId);
            return Ok(carrier);
        }

        [HttpPost]
        [Authorize(Roles = Role.CompanyAdmin)]
        public async Task<IActionResult> AddCarrier([FromBody] AddCarrierDto carrierDto)
        {
            try
            {
                return Ok(await _carrierService.AddCarrier(carrierDto));
            }
            catch (UsernameAlreadyTakenException)
            {
                return BadRequest(AddModelStateError("username", ErrorMessagesConstants.USERNAME_ALREADY_TAKEN));
            }
            catch (PasswordsMismatchException)
            {
                return BadRequest(AddModelStateError("password", ErrorMessagesConstants.PASSWORDS_DO_NOT_MATCH));
            }
        }

        [HttpPut]
        [Authorize(Roles = Role.CompanyAdmin)]
        public async Task<IActionResult> UpdateCarrier([FromBody] UpdateCarrierDto updateCarrierDto)
        {
            try
            {
                await _carrierService.UpdateCarrierWithUser(updateCarrierDto);
                return Ok(true);
            }
            catch (UsernameAlreadyTakenException)
            {
                return BadRequest(AddModelStateError("username", ErrorMessagesConstants.USERNAME_ALREADY_TAKEN));
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Role.CompanyAdmin)]
        public async Task<IActionResult> DeleteCarrier(Guid id)
        {
            return Ok(await _carrierService.DeleteCarrier(id));
        }

        private static ModelStateDictionary AddModelStateError(String field, String error)
        {
            ModelStateDictionary modelState = new ModelStateDictionary();
            modelState.TryAddModelError(field, error);
            return modelState;
        }
    }
}
