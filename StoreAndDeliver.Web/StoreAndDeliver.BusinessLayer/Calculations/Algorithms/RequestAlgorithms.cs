using AutoMapper;
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
            //TODO check if in combination is only one item and it is already in another combination, than skip it
            //TODO check carrier capacity
            return resultRequests;
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
