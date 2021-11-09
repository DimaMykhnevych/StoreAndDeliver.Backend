using Microsoft.EntityFrameworkCore;
using StoreAndDeliver.DataLayer.DbContext;
using StoreAndDeliver.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAndDeliver.DataLayer.Repositories.RequestRepository
{
    public class RequestRepository : Repository<Request>, IRequestRepository
    {
        public RequestRepository(StoreAndDeliverDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Request>> GetOutdatedStoreRequests()
        {
            return await context.Requests
                .Where(r => r.StoreUntilDate < DateTime.Now && r.Type == Enums.RequestType.Store)
                .Include(r => r.CargoRequests)
                .ThenInclude(cr => cr.Cargo)
                .Include(r => r.CargoRequests)
                .ThenInclude(cr => cr.Store)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Request>> GetUserRequests(Guid userId)
        {
            return await context.Requests.Where(r => r.AppUserId == userId).ToListAsync();
        }
    }
}
