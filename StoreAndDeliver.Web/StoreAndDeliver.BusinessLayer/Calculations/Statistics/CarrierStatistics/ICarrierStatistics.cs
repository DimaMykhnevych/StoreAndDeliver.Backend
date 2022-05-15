using StoreAndDeliver.BusinessLayer.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Calculations.Statistics.CarrierStatistics
{
    public interface ICarrierStatistics
    {
        Task<IEnumerable<CarrierStatisticsDto>> GetCarrierStatistics();
    }
}
