using Microsoft.EntityFrameworkCore;
using StoreAndDeliver.DataLayer.DbContext;
using StoreAndDeliver.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAndDeliver.DataLayer.Repositories.FeedbackRepository
{
    public class FeedbackRepository : Repository<Feedback>, IFeedbackRepository
    {
        public FeedbackRepository(StoreAndDeliverDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Feedback>> GetUserFeedback(Guid userId)
        {
            return await context.Feedback
                .Where(f => f.AppUserId == userId)
                .Include(f => f.AppUser)
                .ToListAsync();
        }

        public async Task<IEnumerable<Feedback>> GetFeedbackWithUser()
        {
            return await context.Feedback
                .Include(f => f.AppUser)
                .ToListAsync();
        }
    }
}
