using AutoMapper;
using StoreAndDeliver.BusinessLayer.Constants;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.BusinessLayer.Services.ConvertionService;
using StoreAndDeliver.BusinessLayer.Services.EnvironmnetSettingService;
using StoreAndDeliver.DataLayer.Enums;
using StoreAndDeliver.DataLayer.Models;
using StoreAndDeliver.DataLayer.Repositories.CargoRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.CargoService
{
    public class CargoService : ICargoService
    {
        private readonly ICargoRepository _cargoRepository;
        private readonly IConvertionService _convertionService;
        private readonly IEnvironmnetSettingService _environmnetSettingService;
        private readonly IMapper _mapper;

        public CargoService(ICargoRepository cargoRepository, 
            IConvertionService convertionService,
            IEnvironmnetSettingService environmnetSettingService,
            IMapper mapper)
        {
            _cargoRepository = cargoRepository;
            _convertionService = convertionService;
            _environmnetSettingService = environmnetSettingService;
            _mapper = mapper;
        }

        public async Task<CargoDto> AddCargo(AddCargoDto addCargoDto, Units units)
        {
            Cargo cargo = _mapper.Map<Cargo>(addCargoDto);
            Cargo existingCargo = await _cargoRepository.Get(cargo.Id);
            if(existingCargo != null)
            {
                return _mapper.Map<CargoDto>(existingCargo);
            }
            ConvertCargoUnits(cargo, units);
            await ConvertCargoSettings(cargo.CargoSettings, units);
            cargo.Id = Guid.NewGuid();
            Cargo addedCargo = await _cargoRepository.Insert(cargo);
            await _cargoRepository.Save();
            return _mapper.Map<CargoDto>(addedCargo);
        }

        public async Task<IEnumerable<CargoDto>> AddCargoRange(IEnumerable<AddCargoDto> addCargoDtos, Units units)
        {
            List<CargoDto> addedCargo = new();
            foreach(var c in addCargoDtos)
            {
                CargoDto added = await AddCargo(c, units);
                addedCargo.Add(added);
            }
            return addedCargo;
        }

        private async Task ConvertCargoSettings(IEnumerable<CargoSetting> cargoSettingDtos, Units units)
        {
            var envSettings = (await _environmnetSettingService.GetEnvironmentSettings()).ToList();
            var temperatureId = envSettings.FirstOrDefault(s => s.Name == SettingsConstants.TEMPERATURE).Id;
            foreach (var setting in cargoSettingDtos)
            {
                setting.Id = Guid.NewGuid();
                if (setting.EnvironmentSettingId == temperatureId)
                {
                    setting.MaxValue = _convertionService
                        .ConvertTemperature(units.Temperature, TemperatureUnit.Celsius, setting.MaxValue);
                    setting.MinValue = _convertionService
                        .ConvertTemperature(units.Temperature, TemperatureUnit.Celsius, setting.MinValue);
                }
            }
        }

        private void ConvertCargoUnits(Cargo cargo, Units units)
        {
            cargo.Height = _convertionService.ConvertLength(units.Length, LengthUnit.Meters, cargo.Height);
            cargo.Width = _convertionService.ConvertLength(units.Length, LengthUnit.Meters, cargo.Width);
            cargo.Length = _convertionService.ConvertLength(units.Length, LengthUnit.Meters, cargo.Length);
            cargo.Weight = _convertionService.ConvertWeigth(units.Weight, WeightUnit.Kilograms, cargo.Weight);
        }
    }
}
