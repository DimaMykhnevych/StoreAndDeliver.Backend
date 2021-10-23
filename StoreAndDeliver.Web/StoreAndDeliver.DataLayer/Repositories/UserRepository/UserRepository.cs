using StoreAndDeliver.DataLayer.DbContext;
using StoreAndDeliver.DataLayer.Models;

namespace StoreAndDeliver.DataLayer.Repositories.UserRepository
{
    public class UserRepository : Repository<AppUser>, IUserRepository
    {
        public UserRepository(StoreAndDeliverDbContext context) : base(context)
        {
        }
    }
}
