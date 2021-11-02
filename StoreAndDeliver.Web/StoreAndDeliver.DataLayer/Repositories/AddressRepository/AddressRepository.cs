using StoreAndDeliver.DataLayer.DbContext;
using StoreAndDeliver.DataLayer.Models;

namespace StoreAndDeliver.DataLayer.Repositories.AddressRepository
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(StoreAndDeliverDbContext context) : base(context)
        {
        }
    }
}
