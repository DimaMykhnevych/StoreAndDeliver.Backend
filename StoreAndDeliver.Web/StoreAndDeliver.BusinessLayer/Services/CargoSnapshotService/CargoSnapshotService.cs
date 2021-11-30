using AutoMapper;
using StoreAndDeliver.BusinessLayer.Constants;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.BusinessLayer.Services.ConvertionService;
using StoreAndDeliver.DataLayer.Enums;
using StoreAndDeliver.DataLayer.Models;
using StoreAndDeliver.DataLayer.Repositories.CargoRepository;
using StoreAndDeliver.DataLayer.Repositories.CargoSnapshotsRepository;
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
        private readonly IMapper _mapper;

        public CargoSnapshotService(ICargoSnapshotsRepository cargoSnapshotsRepository, 
            IMapper mapper,
            IConvertionService convertionService,
            ICargoRepository cargoRepository)
        {
            _cargoSnapshotsRepository = cargoSnapshotsRepository;
            _convertionService = convertionService;
            _cargoRepository = cargoRepository;
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
            foreach(var k in userCargoSnapshotsDictionary)
            {
                var cargo = await _cargoRepository.GetCargoWithSettings(k.Key);
                userCargoSnapshots.Add(new GetUserCargoSnapshotsDto
                {
                    Cargo = _mapper.Map<CargoDto>(cargo),
                    CargoSnapshots = _mapper.Map<List<CargoSnapshotDto>>(k.Value)
                });
            }
            foreach (var cs in  userCargoSnapshots)
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
