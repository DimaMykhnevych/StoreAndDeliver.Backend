using Microsoft.EntityFrameworkCore;
using StoreAndDeliver.DataLayer.DbContext;
using StoreAndDeliver.DataLayer.Enums;
using StoreAndDeliver.DataLayer.Models;
using System;
using System.Linq;

namespace StoreAndDeliver.DataLayer.Builders.CargoSessionQueryBuilder
{
    public class CargoSessionQueryBuilder : ICargoSessionQueryBuilder
    {
        private readonly StoreAndDeliverDbContext _dbContext;
        private IQueryable<CargoSession> _query;

        public CargoSessionQueryBuilder(StoreAndDeliverDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<CargoSession> Build()
        {
            IQueryable<CargoSession> result = _query;
            _query = null;
            return result;
        }

        public ICargoSessionQueryBuilder SetBaseCargoSessionInfo()
        {
            _query = _dbContext.CargoSessions
                .Include(cr => cr.CargoRequest)
                .ThenInclude(cr => cr.Request)
                .ThenInclude(cr => cr.FromAddress)
                .Include(cr => cr.CargoRequest.Request.ToAddress)
                .Include(cr => cr.CargoRequest)
                .ThenInclude(cr => cr.Cargo)
                .ThenInclude(cr => cr.CargoSettings)
                .ThenInclude(cs => cs.EnvironmentSetting)
                .Include(r => r.CargoRequest)
                .ThenInclude(cr => cr.Store)
                .ThenInclude(cr => cr.Address)
                .AsNoTracking();
            return this;
        }

        public ICargoSessionQueryBuilder SetCargoSessionCarrier(Guid id)
        {
            _query = _query.Where(c => c.CarrierId == id);
            return this;
        }

        public ICargoSessionQueryBuilder SetCargoSessionRequestStatus(RequestStatus? requestStatus)
        {
            if (requestStatus.HasValue)
            {
                _query = _query.Where(r => r.CargoRequest.Status == requestStatus);
            }
            return this;
        }

        public ICargoSessionQueryBuilder SetCargoSessionRequestType(RequestType? requestType)
        {
            if (requestType.HasValue)
            {
                _query = _query.Where(r => r.CargoRequest.Request.Type == requestType);
            }
            return this;
        }
    }
}
