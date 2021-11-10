using StoreAndDeliver.DataLayer.Models;
using System.Collections.Generic;

namespace StoreAndDeliver.BusinessLayer.Calculations.Algorithms
{
    public interface IRequestAlgorithms
    {
        ICollection<ICollection<CargoRequest>> GetOptimizedRequests(ICollection<CargoRequest> cargoRequests);
    }
}
