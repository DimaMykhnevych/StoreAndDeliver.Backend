using StoreAndDeliver.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAndDeliver.DataLayer.Repositories.CargoSessionNoteRepository
{
    public interface ICargoSessionNoteRepository : IRepository<CargoSessionNote>
    {
        Task<IEnumerable<CargoSessionNote>> GetNotesByCargoRequestId(Guid cargoRequestId);
    }
}
