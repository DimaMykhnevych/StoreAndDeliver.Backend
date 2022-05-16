using Microsoft.EntityFrameworkCore;
using StoreAndDeliver.DataLayer.DbContext;
using StoreAndDeliver.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAndDeliver.DataLayer.Repositories.CargoRepository
{
    public class CargoRepository : Repository<Cargo>, ICargoRepository
    {
        public CargoRepository(StoreAndDeliverDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Cargo>> GetCargo()
        {
            return await context.Cargo
                .Include(c => c.CargoSettings)
                .ThenInclude(c => c.EnvironmentSetting)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Cargo> GetCargoWithSettings(Guid cargoId)
        {
            return await context.Cargo
                .Include(c => c.CargoSettings)
                .ThenInclude(c => c.EnvironmentSetting)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == cargoId);
        }
    }
}
