using AutoMapper;
using StoreAndDeliver.BusinessLayer.Constants;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.BusinessLayer.Services.ConvertionService;
using StoreAndDeliver.DataLayer.Enums;
using StoreAndDeliver.DataLayer.Models;
using StoreAndDeliver.DataLayer.Repositories.CargoSnapshotsRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.CargoSnapshotService
{
    public class CargoSnapshotService : ICargoSnapshotService
    {
        private readonly ICargoSnapshotsRepository _cargoSnapshotsRepository;
        private readonly IConvertionService _convertionService;
        private readonly IMapper _mapper;

        public CargoSnapshotService(ICargoSnapshotsRepository cargoSnapshotsRepository, 
            IMapper mapper,
            IConvertionService convertionService)
        {
            _cargoSnapshotsRepository = cargoSnapshotsRepository;
            _convertionService = convertionService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CargoSnapshotDto>> GetCargoSnapshotsByCargoRequestId(GetCargoSnapshotDto getCargoSnapshotDto)
        {
            IEnumerable<CargoSnapshot> cargoSnapshots = 
                await _cargoSnapshotsRepository.GetCargoSnapshotsByCargoRequestId(getCargoSnapshotDto.CargoRequestId);
            ConvertCargoSnapshots(cargoSnapshots, getCargoSnapshotDto);
            return _mapper.Map<IEnumerable<CargoSnapshotDto>>(cargoSnapshots);
        }

        public async Task<IEnumerable<CargoSnapshotDto>> GetCargoSnapshotsByCargoSessionId(Guid cargoSessionId)
        {
            IEnumerable<CargoSnapshot> cargoSnapshots =
                await _cargoSnapshotsRepository.GetCargoSnapshotsByCargoSessionId(cargoSessionId);
            return _mapper.Map<IEnumerable<CargoSnapshotDto>>(cargoSnapshots);
        }

        public async Task<CargoSnapshotDto> AddCargoSnapshot(AddCargoSnapshotDto addCargoSnapshotDto)
        {
            CargoSnapshot cargoSnapshot = _mapper.Map<CargoSnapshot>(addCargoSnapshotDto);
            cargoSnapshot.Id = Guid.NewGuid();
            CargoSnapshot added = await _cargoSnapshotsRepository.Insert(cargoSnapshot);
            await _cargoSnapshotsRepository.Save();
            return _mapper.Map<CargoSnapshotDto>(added);
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
                if(snapshot.EnvironmentSetting.Name == SettingsConstants.TEMPERATURE)
                {
                    snapshot.Value = _convertionService
                        .ConvertTemperature(unitsFrom.Temperature, cargoSnapshotDto.Temperature, snapshot.Value);
                }
            }
        }
    }
}
