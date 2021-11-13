using Microsoft.EntityFrameworkCore;
using StoreAndDeliver.DataLayer.DbContext;
using StoreAndDeliver.DataLayer.Models;
using System.Collections.Generic;
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
    }
}
