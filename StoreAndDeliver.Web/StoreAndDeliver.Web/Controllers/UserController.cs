using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using StoreAndDeliver.BusinessLayer.Constants;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.BusinessLayer.Exceptions;
using StoreAndDeliver.BusinessLayer.Services.UserService;
using StoreAndDeliver.DataLayer.Models;
using System;
using System.Threading.Tasks;

namespace StoreAndDeliver.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> Get(string username)
        {
            return Ok(await _service.GetUserByUsername(username));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] CreateUserDto model)
        {
            model.Role = "User";
            var language = Request.Headers["language"];
            try
            {
                return Ok(await _service.CreateUserAsync(model, language, true));
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

        [HttpPost("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailDto confirmEmailDto)
        {
            ConfirmEmailDto confirmEmail = await _service.ConfirmEmail(confirmEmailDto);
            if (confirmEmail == null)
                return BadRequest("Invalid Email Confirmation Request");
            return Ok(confirmEmail);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteUser(id);
            return Ok();
        }

        private static ModelStateDictionary AddModelStateError(String field, String error)
        {
            ModelStateDictionary modelState = new ModelStateDictionary();
            modelState.TryAddModelError(field, error);
            return modelState;
        }
    }
}
