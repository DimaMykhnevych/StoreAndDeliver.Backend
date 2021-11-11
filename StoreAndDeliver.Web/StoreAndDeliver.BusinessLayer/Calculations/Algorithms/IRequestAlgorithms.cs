using StoreAndDeliver.DataLayer.Models;
using System.Collections.Generic;

namespace StoreAndDeliver.BusinessLayer.Calculations.Algorithms
{
    public interface IRequestAlgorithms
    {
        List<List<CargoRequest>> GetOptimizedRouteForCargoRequestGroups(ICollection<ICollection<CargoRequest>> cargoRequests);
        ICollection<ICollection<CargoRequest>> GetOptimizedRequests(ICollection<CargoRequest> cargoRequests);
    }
}
