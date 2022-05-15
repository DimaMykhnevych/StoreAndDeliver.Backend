using StoreAndDeliver.BusinessLayer.DTOs;
using System.Collections.Generic;

namespace StoreAndDeliver.BusinessLayer.Calculations.Statistics
{
    public interface IRequestStatistics
    {
        IEnumerable<RequestStatisticsDto> GetRequestsByCountriesStatistics();
        IEnumerable<RequestStatisticsDto> GetRequestsByCitiesStatistics();
    }
}
