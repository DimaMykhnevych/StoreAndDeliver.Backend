using StoreAndDeliver.BusinessLayer.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Calculations.Algorithms.StoreAlgorithms
{
    public interface IStoreAlgorithms
    {
        Task<IEnumerable<OptimalStoreLocationDto>> GetOptimalStoreLocations();
    }
}
