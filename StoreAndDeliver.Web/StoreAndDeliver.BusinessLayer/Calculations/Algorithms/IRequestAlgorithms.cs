using StoreAndDeliver.DataLayer.Models;
using System.Collections.Generic;

namespace StoreAndDeliver.BusinessLayer.Calculations.Algorithms
{
    public interface IRequestAlgorithms
    {
        List<List<CargoRequest>> GetOptimizedRouteForCargoRequestGroups(List<List<CargoRequest>> cargoRequests);
        List<List<CargoRequest>> GetOptimizedRequests(ICollection<CargoRequest> cargoRequests);
    }
}
