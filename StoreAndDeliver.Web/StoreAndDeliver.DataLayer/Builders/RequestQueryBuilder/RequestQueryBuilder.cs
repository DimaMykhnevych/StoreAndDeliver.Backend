using Microsoft.EntityFrameworkCore;
using StoreAndDeliver.DataLayer.DbContext;
using StoreAndDeliver.DataLayer.Enums;
using StoreAndDeliver.DataLayer.Models;
using System;
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
                .Include(r => r.FromAddress)
                .Include(r => r.ToAddress)
                .Include(r => r.CargoRequests)
                .ThenInclude(cr => cr.Cargo)
                .ThenInclude(cr => cr.CargoSettings)
                .ThenInclude(cs => cs.EnvironmentSetting)
                .AsNoTracking()
                .Include(r => r.CargoRequests)
                .ThenInclude(cr => cr.Store)
                .ThenInclude(cr => cr.Address)
                .AsNoTracking();

            return this;
        }

        public IRequestQueryBuilder SetRequestId(Guid id)
        {
            _query = _query.Where(r => r.Id == id);
            return this;
        }

        public IRequestQueryBuilder SetCarryOutBeforeDate(DateTime? time)
        {
            if(time != null)
            {
                _query = _query.Where(r => r.CarryOutBefore >= time);
            }
            return this;
        }

        public IRequestQueryBuilder SetStoreDates(DateTime? storeFrom, DateTime? storeUntil)
        {
            if (storeFrom == null && storeUntil != null)
            {
                _query = _query.Where(tp => tp.StoreUntilDate <= storeUntil);
            }
            else if (storeUntil == null && storeFrom != null)
            {
                _query = _query.Where(tp => tp.StoreFromDate >= storeFrom);
            }
            else if (storeFrom != null && storeUntil != null)
            {
                _query = _query.Where(tp => tp.StoreFromDate >= storeFrom
                && tp.StoreUntilDate <= storeUntil);
            }
            return this;
        }

        public IRequestQueryBuilder SetRequestType(RequestType? type)
        {
            if (type != null)
            {
                _query = _query.Where(r => r.Type == type);
            }
            return this;
        }

        public IRequestQueryBuilder SortByDeliverByDate()
        {
            _query = _query.OrderBy(r => r.CarryOutBefore);
            return this;
        }

        //public IRequestQueryBuilder SetRequestStatus(RequestStatus status)
        //{
        //    _query = _query.Where(r => r.CargoRequests == status);
        //    return this;
        //}
    }
}
