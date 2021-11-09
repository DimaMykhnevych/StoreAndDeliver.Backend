using StoreAndDeliver.BusinessLayer.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.StoreService
{
    public interface IStoreService
    {
        Task<bool> DistrubuteCargoByStores(IEnumerable<CargoDto> cargo, RequestDto request);
    }
}
