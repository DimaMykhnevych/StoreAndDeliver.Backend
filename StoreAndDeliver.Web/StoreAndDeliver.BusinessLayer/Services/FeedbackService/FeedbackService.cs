using AutoMapper;
using Microsoft.Extensions.Logging;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.DataLayer.Models;
using StoreAndDeliver.DataLayer.Repositories.FeedbackRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.FeedbackService
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public FeedbackService(IFeedbackRepository feedbackRepository, IMapper mapper, ILoggerFactory loggerFactory)
        {
            _feedbackRepository = feedbackRepository;
            _mapper = mapper;
            _logger = loggerFactory?.CreateLogger(nameof(FeedbackService));
        }

        public async Task<IEnumerable<GetFeedbackDto>> GetFeedbackWithUser()
        {
            _logger.LogInformation("Start getting feedback with user");
            IEnumerable<Feedback> feedback = await _feedbackRepository.GetFeedbackWithUser();
            return _mapper.Map<IEnumerable<GetFeedbackDto>>(feedback);
        }

        public async Task<IEnumerable<GetFeedbackDto>> GetUserFeedback(Guid userId)
        {
            _logger.LogInformation($"Start getting user feedback. User Id: {userId}");
            IEnumerable<Feedback> feedback = await _feedbackRepository.GetUserFeedback(userId);
            return _mapper.Map<IEnumerable<GetFeedbackDto>>(feedback);
        }

        public async Task<GetFeedbackDto> AddFeedback(Guid appUserId, AddFeedbackDto addFeedbackDto)
        {
            Feedback feedbackToAdd = new()
            {
                Content = addFeedbackDto.Content,
                Rating = addFeedbackDto.Rating,
                Date = DateTime.Now,
                AppUserId = appUserId
            };

            try
            {
                _logger.LogInformation("Trying to add feedback");
                Feedback result = await _feedbackRepository.Insert(feedbackToAdd);
                await _feedbackRepository.Save();
                _logger.LogInformation("Successfully added feedback");
                return _mapper.Map<GetFeedbackDto>(result);
            } 
            catch(Exception ex)
            {
                _logger.LogError($"Error during adding feedback: {ex.Message}");
                return null;
            }
        }
    }
}
