using Microsoft.EntityFrameworkCore;
using StoreAndDeliver.DataLayer.DbContext;
using StoreAndDeliver.DataLayer.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAndDeliver.DataLayer.Repositories.StoreRepository
{
    public class StoreRepository : Repository<Store>, IStoreRepository
    {
        public StoreRepository(StoreAndDeliverDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Store>> GetStoresWithAddress()
        {
            return await context.Stores.Include(s => s.Address).AsNoTracking().ToListAsync();
        }

        public async Task UpdateStore(Store store)
        {
            var local = context.Set<Store>()
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(store.Id));

            // check if local is not null 
            if (local != null)
            {
                // detach
                context.Entry(local).State = EntityState.Detached;
            }
            // set Modified flag in your entry
            context.Entry(store).State = EntityState.Modified;

            // save 
            await context.SaveChangesAsync();
        }
    }
}
