using StoreAndDeliver.DataLayer.Models;
using System;
using System.Threading.Tasks;

namespace StoreAndDeliver.DataLayer.Repositories.CarrierRepository
{
    public interface ICarrierRepository : IRepository<Carrier>
    {
        Task<Carrier> GetCarrier(Guid id);
    }
}
