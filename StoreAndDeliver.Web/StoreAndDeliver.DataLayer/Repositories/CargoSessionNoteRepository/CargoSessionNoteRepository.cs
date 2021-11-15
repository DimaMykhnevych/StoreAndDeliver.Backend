using Microsoft.EntityFrameworkCore;
using StoreAndDeliver.DataLayer.DbContext;
using StoreAndDeliver.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAndDeliver.DataLayer.Repositories.CargoSessionNoteRepository
{
    public class CargoSessionNoteRepository : Repository<CargoSessionNote>, ICargoSessionNoteRepository
    {
        public CargoSessionNoteRepository(StoreAndDeliverDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<CargoSessionNote>> GetNotesByCargoRequestId(Guid cargoRequestId)
        {
            return await context.CargoSessionNotes
                .Include(cs => cs.CargoSession)
                .AsNoTracking()
                .Where(n => n.CargoSession.CargoRequestId == cargoRequestId)
                .ToListAsync();
        }
    }
}
