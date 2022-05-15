using Microsoft.Extensions.Logging;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.DataLayer.Repositories.CarrierRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Calculations.Statistics.CarrierStatistics
{
    public class CarrierStatistics : ICarrierStatistics
    {
        private readonly ICarrierRepository _carrierRepository;
        private readonly ILogger _logger;

        public CarrierStatistics(ICarrierRepository carrierRepository, ILoggerFactory loggerFactory)
        {
            _carrierRepository = carrierRepository;
            _logger = loggerFactory?.CreateLogger("CarrierStatistics");
        }

        public async Task<IEnumerable<CarrierStatisticsDto>> GetCarrierStatistics()
        {
            _logger.LogInformation("Start getting carrier statistics");
            var carriers = await _carrierRepository.GetCarrierWithCargoSessions();

            return carriers
                .Select(c => new CarrierStatisticsDto()
                {
                    Name = c.AppUser.UserName,
                    Value = c.CargoSeesions.Count
                })
                .Where(c => c.Value > 0)
                .OrderByDescending(c => c.Value)
                .Take(5);
        }
    }
}
