using StoreAndDeliver.BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.FeedbackService
{
    public interface IFeedbackService
    {
        Task<IEnumerable<GetFeedbackDto>> GetUserFeedback(Guid userId);
        Task<IEnumerable<GetFeedbackDto>> GetFeedbackWithUser();
        Task<GetFeedbackDto> AddFeedback(Guid appUserId, AddFeedbackDto addFeedbackDto);
    }
}
