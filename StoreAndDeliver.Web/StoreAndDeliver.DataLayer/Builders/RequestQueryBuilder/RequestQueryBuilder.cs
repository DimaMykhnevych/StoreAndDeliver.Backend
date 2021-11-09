using Microsoft.EntityFrameworkCore;
using StoreAndDeliver.DataLayer.DbContext;
using StoreAndDeliver.DataLayer.Enums;
using StoreAndDeliver.DataLayer.Models;
using System.Linq;

namespace StoreAndDeliver.DataLayer.Builders.RequestQueryBuilder
{
    public class RequestQueryBuilder : IRequestQueryBuilder
    {
        private readonly StoreAndDeliverDbContext _dbContext;
        private IQueryable<Request> _query;

        public RequestQueryBuilder(StoreAndDeliverDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Request> Build()
        {
            IQueryable<Request> result = _query;
            _query = null;
            return result;
        }

        public IRequestQueryBuilder SetBaseRequestInfo()
        {
            _query = _dbContext.Requests
                .Include(r => r.CargoRequests)
                .ThenInclude(cr => cr.Cargo)
                .ThenInclude(cr => cr.CargoSettings)
                .Include(r => r.CargoRequests)
                .ThenInclude(cr => cr.Store)
                .AsNoTracking();

            return this;
        }

        public IRequestQueryBuilder SetCarryOutBeforeDate()
        {
            throw new System.NotImplementedException();
        }

        public IRequestQueryBuilder SetStoreUntilDate()
        {
            throw new System.NotImplementedException();
        }

        public IRequestQueryBuilder SetRequestType(RequestType type)
        {
            _query = _query.Where(r => r.Type == type);
            return this;
        }

        public IRequestQueryBuilder SortByDeliverByDate()
        {
            _query = _query.OrderBy(r => r.CarryOutBefore);
            return this;
        }
    }
}
