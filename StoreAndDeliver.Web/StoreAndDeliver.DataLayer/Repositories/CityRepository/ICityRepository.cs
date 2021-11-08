using StoreAndDeliver.DataLayer.Models;
using System.Threading.Tasks;

namespace StoreAndDeliver.DataLayer.Repositories.CityRepository
{
    public interface ICityRepository : IRepository<City>
    {
        Task<City> GetCityByAddress(Address address);
    }
}
