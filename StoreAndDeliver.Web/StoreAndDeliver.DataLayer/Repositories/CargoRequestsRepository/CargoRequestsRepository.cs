using StoreAndDeliver.DataLayer.DbContext;
using StoreAndDeliver.DataLayer.Models;

namespace StoreAndDeliver.DataLayer.Repositories.CargoRequestsRepository
{
    public class CargoRequestsRepository : Repository<CargoRequest>, ICargoRequestsRepository
    {
        public CargoRequestsRepository(StoreAndDeliverDbContext context) : base(context)
        {
        }
    }
}
