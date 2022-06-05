using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreAndDeliver.BusinessLayer.Constants;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.BusinessLayer.Services.FeedbackService;
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
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpGet]
        [Route("getFeedbackWithUser")]
        [Authorize(Roles = Role.CompanyAdmin)]
        public async Task<IActionResult> GetFeedbackWithUser()
        {
            IEnumerable<GetFeedbackDto> feedback = await _feedbackService.GetFeedbackWithUser();
            return Ok(feedback);
        }

        [HttpGet]
        [Route("GetUserFeedback")]
        [Authorize(Roles = Role.User)]
        public async Task<IActionResult> GetUserFeedback()
        {
            var userId = new Guid(User.FindFirstValue(AuthorizationConstants.ID));
            IEnumerable<GetFeedbackDto> feedback = await _feedbackService.GetUserFeedback(userId);
            return Ok(feedback);
        }

        [HttpPost]
        [Authorize(Roles = Role.User)]
        public async Task<IActionResult> AddFeedback([FromBody] AddFeedbackDto addFeedbackDto)
        {
            var userId = new Guid(User.FindFirstValue(AuthorizationConstants.ID));
            GetFeedbackDto result = await _feedbackService.AddFeedback(userId, addFeedbackDto);
            if(result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
