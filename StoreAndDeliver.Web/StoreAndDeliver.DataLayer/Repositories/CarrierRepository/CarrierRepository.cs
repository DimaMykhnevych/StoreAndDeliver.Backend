using Microsoft.EntityFrameworkCore;
using StoreAndDeliver.DataLayer.DbContext;
using StoreAndDeliver.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAndDeliver.DataLayer.Repositories.CarrierRepository
{
    public class CarrierRepository : Repository<Carrier>, ICarrierRepository
    {
        public CarrierRepository(StoreAndDeliverDbContext context) : base(context)
        {
        }

        public async Task<Carrier> GetCarrier(Guid id)
        {
            return await context.Carriers.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Carrier> GetCarrierWithUser(Guid carrierId)
        {
            return await context.Carriers
                .Include(c => c.AppUser)
                .FirstOrDefaultAsync(c => c.Id == carrierId);
        }

        public async Task<Carrier> GetCarrierByAppUserId(Guid id)
        {
            return await context.Carriers.FirstOrDefaultAsync(c => c.AppUserId == id);
        }

        public async Task<IEnumerable<Carrier>> GetCarriers()
        {
            return await context.Carriers.Include(c => c.AppUser).ToListAsync();
        }

        public async Task<IEnumerable<Carrier>> GetCarrierWithCargoSessions() {
            return await context.Carriers
                .Include(c => c.AppUser)
                .Include(c => c.CargoSeesions)
                .ToListAsync();
        }

        public async Task UpdateCarrier(Carrier carrier)
        {
            var local = context.Set<Carrier>()
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(carrier.Id));

            if (local != null)
            {
                context.Entry(local).State = EntityState.Detached;
            }

            context.Entry(carrier).State = EntityState.Modified;

            await context.SaveChangesAsync();
        }
    }
}
