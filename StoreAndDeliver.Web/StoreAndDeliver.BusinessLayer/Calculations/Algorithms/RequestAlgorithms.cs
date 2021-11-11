using AutoMapper;
using GeoCoordinatePortable;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.BusinessLayer.Services.CargoService;
using StoreAndDeliver.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreAndDeliver.BusinessLayer.Calculations.Algorithms
{
    public class RequestAlgorithms : IRequestAlgorithms
    {
        private readonly ICargoService _cargoService;
        private readonly IMapper _mapper;

        public RequestAlgorithms(ICargoService cargoService, IMapper mapper)
        {
            _cargoService = cargoService;
            _mapper = mapper;
        }

        public List<List<CargoRequest>> GetOptimizedRouteForCargoRequestGroups(ICollection<ICollection<CargoRequest>> cargoRequests)
        {
            List<List<CargoRequest>> cargoOprimizedByDistance = new List<List<CargoRequest>>();
            foreach(var group in cargoRequests)
            {
                var optimizedGroup = CalculateOptimalRouteForCargoRequests(group.ToList());
                cargoOprimizedByDistance.Add(optimizedGroup);
            }
            return cargoOprimizedByDistance;
        }

        public ICollection<ICollection<CargoRequest>> GetOptimizedRequests(ICollection<CargoRequest> cargoRequests)
        {
            var requestCombinations = GetAllPossibleCargoRequestsCombinations(cargoRequests);
            var resultRequests = new List<ICollection<CargoRequest>>();
            bool isCombinationInconsistency = false;
            foreach (var combination in requestCombinations)
            {
                var cargoDtos = _mapper.Map<IEnumerable<CargoDto>>(combination.Select(c => c.Cargo));
                var settingBounds = _cargoService
                    .GetCargoSettingsBound(cargoDtos);
                foreach (var cr in combination)
                {
                    foreach (var setting in cr.Cargo.CargoSettings)
                    {
                        var minBoundValue = settingBounds[setting.EnvironmentSetting.Name].MinValue;
                        var maxBoundValue = settingBounds[setting.EnvironmentSetting.Name].MaxValue;
                        if (!(minBoundValue >= setting.MinValue && maxBoundValue <= setting.MaxValue))
                        {
                            isCombinationInconsistency = true;
                            break;
                        }
                    }
                    if (isCombinationInconsistency)
                    {
                        break;
                    }
                }
                resultRequests.Add(combination);
            }

            // Check if in combination is only one item and it is already in another combination, than skip it
            for(int i = 0; i < resultRequests.Count; i++)
            {
                if(resultRequests[i].Count == 1 
                    && resultRequests
                    .Where(r => 
                        r.Where(cr => cr.Id == resultRequests[i].First().Id)
                         .Any())
                    .Count() > 1)
                {
                    resultRequests.Remove(resultRequests[i]);
                    i--;
                }
            }
            return resultRequests;
        }

        private List<CargoRequest> CalculateOptimalRouteForCargoRequests(List<CargoRequest> cargoRequests)
        {
            List<CargoRequest> result = new();
            result.Add(cargoRequests[0]);
            cargoRequests.Remove(cargoRequests[0]);
            CargoRequest nextCargoRequest = new();
            for (int i = 0; i < cargoRequests.Count; i++)
            {
                var previousToAddress = new GeoCoordinate
                    (result[i].Request.ToAddress.Latitude, result[i].Request.ToAddress.Longtitude);
                double minDistance = Double.MaxValue;
                for (int j = 0; j < cargoRequests.Count; j++)
                {
                    var nextFromAddress = new GeoCoordinate
                        (cargoRequests[j].Request.FromAddress.Latitude, cargoRequests[j].Request.FromAddress.Longtitude);
                    var distance = previousToAddress.GetDistanceTo(nextFromAddress);
                    if(distance < minDistance)
                    {
                        minDistance = distance;
                        nextCargoRequest = cargoRequests[j];
                    }
                }
                result.Add(nextCargoRequest);
                cargoRequests.Remove(nextCargoRequest);
            }
            return result;
        }

        private static ICollection<ICollection<CargoRequest>> GetAllPossibleCargoRequestsCombinations(ICollection<CargoRequest> cargoRequests)
        {
            double count = Math.Pow(2, cargoRequests.Count);
            List<List<CargoRequest>> result = new List<List<CargoRequest>>();
            var requestsList = cargoRequests.ToList();
            for (int i = 1; i <= count - 1; i++)
            {
                result.Add(new List<CargoRequest>());
                for (int j = 0; j < cargoRequests.Count; j++)
                {
                    var bit = (i & (1 << j)) != 0;
                    if (bit)
                    {
                        result[i - 1].Add(requestsList[j]);
                    }
                }
            }
            return result.ToArray();
        }
    }
}
