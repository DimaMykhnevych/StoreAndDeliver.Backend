using StoreAndDeliver.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAndDeliver.DataLayer.Repositories.RequestRepository
{
    public interface IRequestRepository : IRepository<Request>
    {
        Task<IEnumerable<Request>> GetUserRequests(Guid userId);
    }
}
