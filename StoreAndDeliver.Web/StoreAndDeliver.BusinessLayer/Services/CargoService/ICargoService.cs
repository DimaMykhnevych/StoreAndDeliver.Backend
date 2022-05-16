using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.DataLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.CargoService
{
    public interface ICargoService
    {
        Task<IEnumerable<RecommendedSettingsDto>> GetRecommendationCargoSettings(GetRecommendedSettingsDto recommendedSettingsDto);
        Task<CargoDto> AddCargo(AddCargoDto addCargoDto, Units units);
        Task<IEnumerable<CargoDto>> AddCargoRange(IEnumerable<AddCargoDto> addCargoDtos, Units units);
        Dictionary<string, SettingsBoundDto> GetCargoSettingsBound(IEnumerable<CargoDto> cargos);
        void ConvertCargoUnits(Cargo cargo, Units unitsFrom, Units unitsTo);
        Task ConvertCargoSettings(IEnumerable<CargoSetting> cargoSettingDtos,
            Units unitsFrom, Units unitsTo);
    }
}
