using StoreAndDeliver.BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.StoreService
{
    public interface IStoreService
    {
        Task<IEnumerable<StoreDto>> GetStores();
        Task<StoreDto> CreateStore(AddStoreDto storeDto);
        Task<bool> DistrubuteCargoByStores(IEnumerable<CargoDto> cargo, RequestDto request);
        Task<bool> DeleteStore(Guid id);
    }
}
