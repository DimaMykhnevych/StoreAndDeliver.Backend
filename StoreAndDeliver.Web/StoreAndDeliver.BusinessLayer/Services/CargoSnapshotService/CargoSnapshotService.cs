using AutoMapper;
using StoreAndDeliver.BusinessLayer.Constants;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.BusinessLayer.Services.CargoSessionService;
using StoreAndDeliver.BusinessLayer.Services.CarrierService;
using StoreAndDeliver.BusinessLayer.Services.ConvertionService;
using StoreAndDeliver.DataLayer.Enums;
using StoreAndDeliver.DataLayer.Models;
using StoreAndDeliver.DataLayer.Repositories.CargoRepository;
using StoreAndDeliver.DataLayer.Repositories.CargoSnapshotsRepository;
using StoreAndDeliver.DataLayer.Repositories.EnvironmentSettingReporitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.CargoSnapshotService
{
    public class CargoSnapshotService : ICargoSnapshotService
    {
        private readonly ICargoSnapshotsRepository _cargoSnapshotsRepository;
        private readonly IConvertionService _convertionService;
        private readonly ICargoRepository _cargoRepository;
        private readonly ICarrierService _carrierService;
        private readonly ICargoSessionService _cargoSessionService;
        private readonly IEnvironmentSettingRepository _environmentSettingRepository;
        private readonly IMapper _mapper;

        public CargoSnapshotService(ICargoSnapshotsRepository cargoSnapshotsRepository,
            IMapper mapper,
            IConvertionService convertionService,
            ICarrierService carrierService,
            ICargoRepository cargoRepository,
            ICargoSessionService cargoSessionService,
            IEnvironmentSettingRepository environmentSettingRepository)
        {
            _cargoSnapshotsRepository = cargoSnapshotsRepository;
            _convertionService = convertionService;
            _cargoRepository = cargoRepository;
            _carrierService = carrierService;
            _cargoSessionService = cargoSessionService;
            _environmentSettingRepository = environmentSettingRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CargoSnapshotDto>> GetCargoSnapshotsByCargoRequestId(GetCargoSnapshotDto getCargoSnapshotDto)
        {
            IEnumerable<CargoSnapshot> cargoSnapshots =
                await _cargoSnapshotsRepository.GetCargoSnapshotsByCargoRequestId(getCargoSnapshotDto.CargoRequestId);
            ConvertCargoSnapshots(cargoSnapshots, getCargoSnapshotDto);
            return _mapper.Map<IEnumerable<CargoSnapshotDto>>(cargoSnapshots);
        }

        public async Task<IEnumerable<GetUserCargoSnapshotsDto>> GetUserCargoSnapshots(Guid userId, GetCargoSnapshotDto getCargoSnapshotDto)
        {
            var cargoSnapshots = await _cargoSnapshotsRepository.GetUserCargoSnapshots(userId);
            ConvertCargoSnapshots(cargoSnapshots, getCargoSnapshotDto);
            var userCargoSnapshotsDictionary = cargoSnapshots
                .ToList()
                .GroupBy(cs => cs.CargoSession.CargoRequest.Cargo.Id)
                .ToDictionary(cs => cs.Key, cs => cs.ToList());
            var userCargoSnapshots = new List<GetUserCargoSnapshotsDto>();
            foreach (var k in userCargoSnapshotsDictionary)
            {
                var cargo = await _cargoRepository.GetCargoWithSettings(k.Key);
                var getUserCargoSnapshotDto = new GetUserCargoSnapshotsDto
                {
                    Cargo = _mapper.Map<CargoDto>(cargo),
                    CargoSnapshots = _mapper.Map<List<CargoSnapshotDto>>(k.Value)
                };
                await ConvertCargoTemperatureSetting(getUserCargoSnapshotDto.Cargo.CargoSettings, getCargoSnapshotDto.Temperature);
                userCargoSnapshots.Add(getUserCargoSnapshotDto);
            }
            foreach (var cs in userCargoSnapshots)
            {
                foreach (var snapshot in cs.CargoSnapshots)
                {
                    snapshot.CargoSession = null;
                }
            }
            return userCargoSnapshots;
        }

        public async Task<IEnumerable<CargoSnapshotDto>> GetCargoSnapshotsByCargoSessionId(Guid cargoSessionId)
        {
            IEnumerable<CargoSnapshot> cargoSnapshots =
                await _cargoSnapshotsRepository.GetCargoSnapshotsByCargoSessionId(cargoSessionId);
            return _mapper.Map<IEnumerable<CargoSnapshotDto>>(cargoSnapshots);
        }

        public async Task<bool> AddCurrentCarrierCargoSnapshots(
            Guid appUserId, AddCurrentCarrierCargoSnapshotDto cargoSnapshotDto)
        {
            CarrierDto carrier = await _carrierService.GetCarrierByAppUserId(appUserId);
            var carrierActiveSessions = _cargoSessionService.GetCarrierActiveCargoSessions(carrier.Id, cargoSnapshotDto.RequestType);
            var envSettings = await _environmentSettingRepository.GetAll();
            foreach (var session in carrierActiveSessions)
            {
                var addCargoSnapshotDtos = new List<AddCargoSnapshotDto>
                {
                    new()
                    {
                        CargoSessionId = session.Id,
                        EnvironmentSettingId = envSettings.FirstOrDefault(s => s.Name == SettingsConstants.TEMPERATURE).Id,
                        Value = cargoSnapshotDto.Temperature
                    },
                    new()
                    {
                        CargoSessionId = session.Id,
                        EnvironmentSettingId = envSettings.FirstOrDefault(s => s.Name == SettingsConstants.HUMIDITY).Id,
                        Value = cargoSnapshotDto.Humidity
                    },
                    new()
                    {
                        CargoSessionId = session.Id,
                        EnvironmentSettingId = envSettings.FirstOrDefault(s => s.Name == SettingsConstants.LUMINOSITY).Id,
                        Value = cargoSnapshotDto.Luminosity
                    },
                };
                await AddCargoSnapshotRange(addCargoSnapshotDtos);
            }
            return true;
        }

        public async Task<CargoSnapshotDto> AddCargoSnapshot(AddCargoSnapshotDto addCargoSnapshotDto)
        {
            CargoSnapshot cargoSnapshot = _mapper.Map<CargoSnapshot>(addCargoSnapshotDto);
            cargoSnapshot.Id = Guid.NewGuid();
            CargoSnapshot added = await _cargoSnapshotsRepository.Insert(cargoSnapshot);
            await _cargoSnapshotsRepository.Save();
            return _mapper.Map<CargoSnapshotDto>(added);
        }

        public async Task ConvertCargoTemperatureSetting(IEnumerable<CargoSetting> settings, TemperatureUnit temperature)
        {
            Units unitsFrom = new Units()
            {
                Weight = WeightUnit.Kilograms,
                Length = LengthUnit.Meters,
                Temperature = TemperatureUnit.Celsius,
                Luminosity = LuminosityUnit.Lux,
                Humidity = HumidityUnit.Percentage
            };
            var envSettings = await _environmentSettingRepository.GetAll();
            var temperatureId = envSettings.FirstOrDefault(s => s.Name == SettingsConstants.TEMPERATURE).Id;
            foreach (var setting in settings)
            {
                if (setting.EnvironmentSettingId == temperatureId)
                {
                    setting.MaxValue = _convertionService
                        .ConvertTemperature(unitsFrom.Temperature, temperature, setting.MaxValue);
                    setting.MinValue = _convertionService
                        .ConvertTemperature(unitsFrom.Temperature, temperature, setting.MinValue);
                }
            }

        }

        private async Task<bool> AddCargoSnapshotRange(IEnumerable<AddCargoSnapshotDto> addCargoSnapshotDtos)
        {
            foreach (var snapshot in addCargoSnapshotDtos)
            {
                try
                {
                    await AddCargoSnapshot(snapshot);
                }
                catch
                {
                    return false;
                }
            }

            return true;
        }

        private void ConvertCargoSnapshots(IEnumerable<CargoSnapshot> cargoSnapshots, GetCargoSnapshotDto cargoSnapshotDto)
        {
            Units unitsFrom = new Units()
            {
                Temperature = TemperatureUnit.Celsius,
                Luminosity = LuminosityUnit.Lux,
                Humidity = HumidityUnit.Percentage
            };
            foreach (var snapshot in cargoSnapshots)
            {
                if (snapshot.EnvironmentSetting.Name == SettingsConstants.TEMPERATURE)
                {
                    snapshot.Value = _convertionService
                        .ConvertTemperature(unitsFrom.Temperature, cargoSnapshotDto.Temperature, snapshot.Value);
                }
            }
        }
    }
}
