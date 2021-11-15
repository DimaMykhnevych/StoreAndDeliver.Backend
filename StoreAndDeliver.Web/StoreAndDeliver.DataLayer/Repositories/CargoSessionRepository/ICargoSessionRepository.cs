using StoreAndDeliver.DataLayer.Enums;
using StoreAndDeliver.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAndDeliver.DataLayer.Repositories.CargoSessionRepository
{
    public interface ICargoSessionRepository : IRepository<CargoSession>
    {
        Task<IEnumerable<CargoRequest>> GetCarrierCargoRequests(Guid carrierId, RequestType requestType);
        Task<CargoSession> GetCargoSessionByCargoRequestId(Guid cargoRequestId);
    }
}
