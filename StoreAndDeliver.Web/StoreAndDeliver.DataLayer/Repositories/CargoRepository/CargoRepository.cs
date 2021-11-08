using StoreAndDeliver.DataLayer.DbContext;
using StoreAndDeliver.DataLayer.Models;

namespace StoreAndDeliver.DataLayer.Repositories.CargoRepository
{
    public class CargoRepository : Repository<Cargo>, ICargoRepository
    {
        public CargoRepository(StoreAndDeliverDbContext context) : base(context)
        {
        }
    }
}
