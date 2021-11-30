using Microsoft.EntityFrameworkCore;
using StoreAndDeliver.DataLayer.DbContext;
using StoreAndDeliver.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAndDeliver.DataLayer.Repositories.CargoSnapshotsRepository
{
    public class CargoSnapshotsRepository : Repository<CargoSnapshot>, ICargoSnapshotsRepository
    {
        public CargoSnapshotsRepository(StoreAndDeliverDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<CargoSnapshot>> GetUserCargoSnapshots(Guid userId)
        {
            return await context.CargoSnapshots
                .Include(c => c.EnvironmentSetting)
                .Include(c => c.CargoSession)
                .ThenInclude(c => c.CargoRequest)
                .ThenInclude(c => c.Request)
                .AsNoTracking()
                .Include(c => c.CargoSession)
                .ThenInclude(c => c.CargoRequest)
                .ThenInclude(c => c.Cargo)
                .ThenInclude(c => c.CargoSettings)
                .ThenInclude(c => c.EnvironmentSetting)
                .AsNoTracking()
                .Where(c => c.CargoSession.CargoRequest.Request.AppUserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<CargoSnapshot>> GetCargoSnapshotsByCargoRequestId(Guid cargoRequestId)
        {
            return await context.CargoSnapshots
                .Include(c => c.EnvironmentSetting)
                .Include(c => c.CargoSession)
                .ThenInclude(c => c.CargoRequest)
                .ThenInclude(c => c.Cargo)
                .ThenInclude(c => c.CargoSettings)
                .ThenInclude(c => c.EnvironmentSetting)
                .AsNoTracking()
                .Where(c => c.CargoSession.CargoRequestId == cargoRequestId)
                .ToListAsync();
        }

        public async Task<IEnumerable<CargoSnapshot>> GetCargoSnapshotsByCargoSessionId(Guid cargoSessionId)
        {
            return await context.CargoSnapshots
               .Include(c => c.EnvironmentSetting)
               .Include(c => c.CargoSession)
               .ThenInclude(c => c.CargoRequest)
               .ThenInclude(c => c.Cargo)
               .ThenInclude(c => c.CargoSettings)
               .ThenInclude(c => c.EnvironmentSetting)
               .AsNoTracking()
               .Where(c => c.CargoSessionId == cargoSessionId)
               .ToListAsync();
        }
    }
}
