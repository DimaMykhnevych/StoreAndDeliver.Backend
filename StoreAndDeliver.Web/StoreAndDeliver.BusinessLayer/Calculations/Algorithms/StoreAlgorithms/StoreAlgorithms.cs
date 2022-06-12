using StoreAndDeliver.DataLayer.Builders.RequestQueryBuilder;
using StoreAndDeliver.DataLayer.Enums;
using StoreAndDeliver.DataLayer.Models;
using System.Linq;
using System.Threading.Tasks;
using GeoCoordinatePortable;
using System.Collections.Generic;
using StoreAndDeliver.DataLayer.Repositories.StoreRepository;
using StoreAndDeliver.DataLayer.Builders.CitiesQueryBuilder;
using Microsoft.Extensions.Logging;
using StoreAndDeliver.BusinessLayer.DTOs;
using AutoMapper;

namespace StoreAndDeliver.BusinessLayer.Calculations.Algorithms.StoreAlgorithms
{
    public class StoreAlgorithms : IStoreAlgorithms
    {
        private const int defaultGeoDistance = 2;
        private const int defaultGeoDictionarySize = 2;
        private const int defaultOptimalStoreDistance = 200000;
        private const int defaultOptimalCitiesCount = 3;
        private readonly IRequestQueryBuilder _requestQueryBuilder;
        private readonly IStoreRepository _storeRepository;
        private readonly ICitiesQueryBuilder _citiesQueryBuilder;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public StoreAlgorithms(IRequestQueryBuilder requestQueryBuilder, 
            IStoreRepository storeRepository,
            ICitiesQueryBuilder citiesQueryBuilder,
            ILoggerFactory loggerFactory,
            IMapper mapper)
        {
            _requestQueryBuilder = requestQueryBuilder;
            _storeRepository = storeRepository;
            _citiesQueryBuilder = citiesQueryBuilder;
            _mapper = mapper;
            _logger = loggerFactory?.CreateLogger("StoreAlgorithms");
        }

        public async Task<IEnumerable<OptimalStoreLocationDto>> GetOptimalStoreLocations()
        {
            _logger.LogInformation("Start getting optimal store locations");
            var storeRequests = _requestQueryBuilder
                .SetRequestAddressInfo()
                .SetRequestType(RequestType.Store)
                .Build().ToList();

            _logger.LogInformation("Start getting current stores geo range");
            var currentStoresGeoRange = GetCurrentStoresGeoRange(storeRequests);
            _logger.LogInformation($"Current stores geo range amount: {currentStoresGeoRange.Count}");
            _logger.LogInformation("Start getting optimal locations");
            var optimalLocations = await GetOptimalLocations(currentStoresGeoRange);
            _logger.LogInformation($"Optimal Locations amount: {optimalLocations.Count}");
            var result = new List<OptimalStoreLocationDto>();
            foreach(var item in optimalLocations)
            {
                result.Add(new OptimalStoreLocationDto()
                {
                    StartLatitude = item.Key.Key.Latitude,
                    EndLatitude = item.Key.Value.Latitude,
                    StartLongtitude = item.Key.Key.Longitude,
                    EndLongtitude = item.Key.Value.Longitude,
                    Cities = _mapper.Map<IEnumerable<CityDto>>(item.Value)
                });
            }
            _logger.LogInformation("End getting optimal store locations");
            return result;
        }

        private async Task<IDictionary<KeyValuePair<GeoCoordinate, GeoCoordinate>, List<City>>> GetOptimalLocations
            (IDictionary<KeyValuePair<GeoCoordinate, GeoCoordinate>, List<Request>> storesGeoRanges)
        {
            var result = new Dictionary<KeyValuePair<GeoCoordinate, GeoCoordinate>, List<City>>();
            var existingStores = await _storeRepository.GetStoresWithAddress();
            foreach (var item in storesGeoRanges)
            {
                // exclude existing stores
                var optimalLocation = GetRangeOptimalLocation(item.Value)
                    .Where(c => !existingStores
                    .Any(s => s.Address.City == c.CityName && s.Address.Country == c.Country))
                    .Take(defaultOptimalCitiesCount)
                    .ToList();
                result[item.Key] = optimalLocation;
                _logger.LogInformation($"Optimal Locations: {optimalLocation.Count}");
            }

            return result;
        }

        private IEnumerable<City> GetRangeOptimalLocation(IEnumerable<Request> requests)
        {
            var latitudesSum = requests.Select(r => r.FromAddress.Latitude).Sum();
            var longtitudesSum = requests.Select(r => r.FromAddress.Longtitude).Sum();
            
            var middleCoordinates = new GeoCoordinate(
                latitudesSum / requests.Count(), 
                longtitudesSum / requests.Count());

            var cities = _citiesQueryBuilder
                .SetBaseCityInfo()
                .SetCountryName(requests.ElementAt(0).FromAddress.Country)
                .Build().ToList();

            var results = new Dictionary<City, double>();

            foreach(var city in cities)
            {
                var cityCoordinate = new GeoCoordinate(city.Latitude, city.Longtitude);
                var distanceToMiddleStoreCoordinates = cityCoordinate.GetDistanceTo(middleCoordinates);
                if (distanceToMiddleStoreCoordinates <= defaultOptimalStoreDistance)
                {
                    results[city] = distanceToMiddleStoreCoordinates;
                }
            }
            return results.OrderBy(r => r.Value).Select(r => r.Key);
        }

        private static IDictionary<KeyValuePair<GeoCoordinate, GeoCoordinate>, List<Request>> GetCurrentStoresGeoRange
            (IEnumerable<Request> storeRequests)
        {
            var geoDictionary = new Dictionary<KeyValuePair<GeoCoordinate, GeoCoordinate>, List<Request>>();
            // latitude
            for (int i = -90; i <= 90; i += defaultGeoDistance)
            {
                // longtitude
                for (int j = -180; j <= 180; j += defaultGeoDistance)
                {
                    foreach (var request in storeRequests)
                    {
                        var address = request.FromAddress;
                        var isAddressInCurrentGeoRange =
                            (address.Latitude >= i && address.Latitude <= i + defaultGeoDistance)
                            && (address.Longtitude >= j && address.Longtitude <= j + defaultGeoDistance);

                        // if city already presents in list, e.x. latitude = 50
                        var addedRequests = geoDictionary.SelectMany(v => v.Value);
                        if (isAddressInCurrentGeoRange && !addedRequests.Contains(request))
                        {
                            var geoRange = new KeyValuePair<GeoCoordinate, GeoCoordinate>(
                                    new GeoCoordinate(i, j),
                                    new GeoCoordinate(i + defaultGeoDistance, j + defaultGeoDistance));

                            if (geoDictionary.ContainsKey(geoRange))
                            {
                                geoDictionary[geoRange].Add(request);
                            }
                            else
                            {
                                geoDictionary.Add(
                                    new KeyValuePair<GeoCoordinate, GeoCoordinate>(
                                        new GeoCoordinate(i, j), new GeoCoordinate(i + defaultGeoDistance, j + defaultGeoDistance)),
                                    new List<Request>() { request });
                            }

                        }
                    }
                }
            }
            return geoDictionary
                .OrderByDescending(c => c.Value.Count)
                .Take(defaultGeoDictionarySize)
                .ToDictionary(c => c.Key, v => v.Value);
        }
    }
}
