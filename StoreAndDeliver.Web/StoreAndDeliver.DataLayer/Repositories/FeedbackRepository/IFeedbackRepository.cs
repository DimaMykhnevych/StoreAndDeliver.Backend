using StoreAndDeliver.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAndDeliver.DataLayer.Repositories.FeedbackRepository
{
    public interface IFeedbackRepository : IRepository<Feedback>
    {
        Task<IEnumerable<Feedback>> GetUserFeedback(Guid id);
        Task<IEnumerable<Feedback>> GetFeedbackWithUser();
    }
}
