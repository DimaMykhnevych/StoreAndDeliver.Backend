using Microsoft.EntityFrameworkCore;
using StoreAndDeliver.DataLayer.DbContext;
using StoreAndDeliver.DataLayer.Enums;
using StoreAndDeliver.DataLayer.Models;
using System;
using System.Linq;

namespace StoreAndDeliver.DataLayer.Builders.CargoRequestQueryBuilder
{
    public class CargoRequestQueryBuilder : ICargoRequestQueryBuilder
    {
        private readonly StoreAndDeliverDbContext _dbContext;
        private IQueryable<CargoRequest> _query;

        public CargoRequestQueryBuilder(StoreAndDeliverDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<CargoRequest> Build()
        {
            IQueryable<CargoRequest> result = _query;
            _query = null;
            return result;
        }

        public ICargoRequestQueryBuilder SetBaseCargoRequestInfo()
        {
            _query = _dbContext.CargoRequests
                .Include(c => c.Request)
                .ThenInclude(c => c.FromAddress)
                .AsNoTracking()
                .Include(c => c.Request)
                .ThenInclude(c => c.ToAddress)
                .AsNoTracking()
                .Include(c => c.Cargo)
                .ThenInclude(cr => cr.CargoSettings)
                .ThenInclude(cs => cs.EnvironmentSetting)
                .AsNoTracking()
                .Include(c => c.Store)
                .ThenInclude(c => c.Address)
                .AsNoTracking();

            return this;
        }

        public ICargoRequestQueryBuilder SetCargoRequestStatus(RequestStatus? requestStatus)
        {
            if (requestStatus.HasValue)
            {
                _query = _query.Where(cr => cr.Status == requestStatus);
            }
            return this;
        }

        public ICargoRequestQueryBuilder SetCargoRequestType(RequestType? requestType)
        {
            if (requestType.HasValue)
            {
                _query = _query.Where(cr => cr.Request.Type == requestType);
            }
            return this;
        }

        public ICargoRequestQueryBuilder SetCargoRequestUser(Guid id)
        {
            _query = _query.Where(cr => cr.Request.AppUserId == id);
            return this;
        }
    }
}
