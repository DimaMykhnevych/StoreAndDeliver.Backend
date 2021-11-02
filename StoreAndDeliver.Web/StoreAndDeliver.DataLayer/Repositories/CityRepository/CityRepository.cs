using StoreAndDeliver.DataLayer.DbContext;
using StoreAndDeliver.DataLayer.Models;

namespace StoreAndDeliver.DataLayer.Repositories.CityRepository
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(StoreAndDeliverDbContext context) : base(context)
        {
        }
    }
}
