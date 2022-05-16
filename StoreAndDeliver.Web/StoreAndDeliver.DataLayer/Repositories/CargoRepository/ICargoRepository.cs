using StoreAndDeliver.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAndDeliver.DataLayer.Repositories.CargoRepository
{
    public interface ICargoRepository : IRepository<Cargo>
    {
        Task<Cargo> GetCargoWithSettings(Guid cargoId);
        Task<IEnumerable<Cargo>> GetCargo();
    }
}
