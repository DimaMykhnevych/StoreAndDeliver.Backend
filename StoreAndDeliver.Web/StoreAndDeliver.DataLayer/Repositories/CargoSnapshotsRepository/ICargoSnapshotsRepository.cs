using StoreAndDeliver.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAndDeliver.DataLayer.Repositories.CargoSnapshotsRepository
{
    public interface ICargoSnapshotsRepository : IRepository<CargoSnapshot>
    {
        Task<IEnumerable<CargoSnapshot>> GetUserCargoSnapshots(Guid userId);
        Task<IEnumerable<CargoSnapshot>> GetCargoSnapshotsByCargoRequestId(Guid cargoRequestId);
        Task<IEnumerable<CargoSnapshot>> GetCargoSnapshotsByCargoSessionId(Guid cargoSessionId);
    }
}
