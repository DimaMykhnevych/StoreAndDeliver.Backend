using Microsoft.EntityFrameworkCore;
using StoreAndDeliver.DataLayer.DbContext;
using StoreAndDeliver.DataLayer.Models;
using System;
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

        public async Task<Carrier> GetCarrierByAppUserId(Guid id)
        {
            return await context.Carriers.FirstOrDefaultAsync(c => c.AppUserId == id);
        }

        public async Task UpdateCarrier(Carrier carrier)
        {
            var local = context.Set<Carrier>()
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(carrier.Id));

            // check if local is not null 
            if (local != null)
            {
                // detach
                context.Entry(local).State = EntityState.Detached;
            }
            // set Modified flag in your entry
            context.Entry(carrier).State = EntityState.Modified;

            // save 
            await context.SaveChangesAsync();
        }
    }
}
