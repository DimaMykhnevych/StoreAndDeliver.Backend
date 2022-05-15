using StoreAndDeliver.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAndDeliver.DataLayer.Repositories.CarrierRepository
{
    public interface ICarrierRepository : IRepository<Carrier>
    {
        Task<Carrier> GetCarrier(Guid id);
        Task<IEnumerable<Carrier>> GetCarrierWithCargoSessions();
        Task<Carrier> GetCarrierByAppUserId(Guid id);
        Task<Carrier> GetCarrierWithUser(Guid carrierId);
        Task<IEnumerable<Carrier>> GetCarriers();
        Task UpdateCarrier(Carrier carrier);
    }
}
