using Microsoft.EntityFrameworkCore;
using StoreAndDeliver.DataLayer.DbContext;
using StoreAndDeliver.DataLayer.Enums;
using StoreAndDeliver.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAndDeliver.DataLayer.Repositories.CargoSessionRepository
{
    public class CargoSessionRepository : Repository<CargoSession>, ICargoSessionRepository
    {
        public CargoSessionRepository(StoreAndDeliverDbContext context) : base(context)
        {
        }

        public async Task<CargoSession> GetCargoSessionByCargoRequestId(Guid cargoRequestId)
        {
            return await context.CargoSessions
                .FirstOrDefaultAsync(cs => cs.CargoRequestId == cargoRequestId);
        }


        public async Task<IEnumerable<CargoRequest>> GetCarrierCargoRequests(Guid carrierId, RequestType requestType)
        {
            return await context.CargoSessions
                .Where(cs => cs.CarrierId == carrierId)
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
                .AsNoTracking()
                .Select(c => c.CargoRequest)
                .Where(c => c.Request.Type == requestType)
                .ToListAsync();
        }
    }
}
