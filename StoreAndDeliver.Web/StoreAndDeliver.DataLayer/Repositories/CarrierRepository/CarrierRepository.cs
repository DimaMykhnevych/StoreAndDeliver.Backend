using Microsoft.EntityFrameworkCore;
using StoreAndDeliver.DataLayer.DbContext;
using StoreAndDeliver.DataLayer.Models;
using System;
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
    }
}
