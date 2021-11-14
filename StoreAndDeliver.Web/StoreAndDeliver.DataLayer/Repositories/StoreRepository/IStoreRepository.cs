using StoreAndDeliver.DataLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAndDeliver.DataLayer.Repositories.StoreRepository
{
    public interface IStoreRepository : IRepository<Store>
    {
        Task<IEnumerable<Store>> GetStoresWithAddress();
        Task UpdateStore(Store store);
    }
}
