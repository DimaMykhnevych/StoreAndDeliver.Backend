using Microsoft.EntityFrameworkCore;
using StoreAndDeliver.DataLayer.DbContext;
using StoreAndDeliver.DataLayer.Models;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAndDeliver.DataLayer.Repositories.CityRepository
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(StoreAndDeliverDbContext context) : base(context)
        {
        }

        public async Task<City> GetCityByAddress(Address address)
        {
            return await context.Cities
                .FirstOrDefaultAsync(c => c.CityName == address.City && c.Country == address.Country);
        }
    }
}
