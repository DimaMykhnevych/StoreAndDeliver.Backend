using Microsoft.Extensions.Logging;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.DataLayer.Builders.RequestQueryBuilder;
using System.Collections.Generic;
using System.Linq;

namespace StoreAndDeliver.BusinessLayer.Calculations.Statistics
{
    public class RequestStatistics : IRequestStatistics
    {
        private readonly IRequestQueryBuilder _requestQueryBuilder;
        private readonly ILogger _logger;

        public RequestStatistics(IRequestQueryBuilder requestQueryBuilder, ILoggerFactory loggerFactory)
        {
            _requestQueryBuilder = requestQueryBuilder;
            _logger = loggerFactory?.CreateLogger("RequestStatistics");
        }

        public IEnumerable<RequestStatisticsDto> GetRequestsByCountriesStatistics()
        {
            _logger.LogInformation("Start getting requests by countries statistics");
            var requests = _requestQueryBuilder
                .SetRequestAddressInfo()
                .Build().ToList();

            return requests
                .GroupBy(r => r.FromAddress.Country)
                .Select(r => new RequestStatisticsDto() 
                {
                    Name = r.Key,
                    Value = r.ToList().Count
                })
                .OrderByDescending(r => r.Value)
                .Take(5);
        }

        public IEnumerable<RequestStatisticsDto> GetRequestsByCitiesStatistics()
        {
            _logger.LogInformation("Start getting requests by cities statistics");
            var requests = _requestQueryBuilder
                .SetRequestAddressInfo()
                .Build().ToList();

            return requests
                .GroupBy(r => r.FromAddress.City)
                .Select(r => new RequestStatisticsDto()
                {
                    Name = r.Key,
                    Value = r.ToList().Count
                })
                .OrderByDescending(r => r.Value)
                .Take(5);
        }
    }
}
