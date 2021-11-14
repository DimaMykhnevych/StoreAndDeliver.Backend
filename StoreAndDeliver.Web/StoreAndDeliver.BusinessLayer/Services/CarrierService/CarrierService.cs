using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.BusinessLayer.Exceptions;
using StoreAndDeliver.BusinessLayer.Services.UserService;
using StoreAndDeliver.DataLayer.Models;
using StoreAndDeliver.DataLayer.Repositories.CarrierRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.CarrierService
{
    public class CarrierService : ICarrierService
    {
        private readonly ICarrierRepository _carrierRepository;
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;


        public CarrierService(ICarrierRepository carrierRepository,
            IUserService userService,
            UserManager<AppUser> userManager,
            IMapper mapper,
            ILoggerFactory loggerFactory)
        {
            _carrierRepository = carrierRepository;
            _userService = userService;
            _userManager = userManager;
            _mapper = mapper;

            _logger = loggerFactory?.CreateLogger("Carrier Service");
        }

        public async Task<CarrierDto> GetCarrier(Guid id)
        {
            _logger.LogInformation($"Get Carrier {id}");
            Carrier carrier = await _carrierRepository.GetCarrier(id);
            return _mapper.Map<CarrierDto>(carrier);
        }

        public async Task<CarrierDto> GetCarrierByAppUserId(Guid id)
        {
            Carrier carrier = await _carrierRepository.GetCarrierByAppUserId(id);
            return _mapper.Map<CarrierDto>(carrier);
        }

        public async Task<IEnumerable<CarrierDto>> GetCarriers()
        {
            _logger.LogInformation($"Get Carriers");
            IEnumerable<Carrier> carriers = await _carrierRepository.GetCarriers();
            return _mapper.Map<IEnumerable<CarrierDto>>(carriers);
        }

        public async Task<CarrierDto> AddCarrier(AddCarrierDto addCarrierDto)
        {
            CreateUserDto createUserDto = new CreateUserDto
            {
                UserName = addCarrierDto.UserName,
                Email = addCarrierDto.Email,
                Password = addCarrierDto.Password,
                ConfirmPassword = addCarrierDto.Password,
                Role = Role.Carrier
            };
            AppUser addedUser = await _userService.CreateUserAsync(createUserDto, "en", false);
            Carrier carrier = new Carrier
            {
                Id = Guid.NewGuid(),
                CompanyName = addCarrierDto.CompanyName,
                MaxCargoVolume = addCarrierDto.MaxCargoVolume,
                CurrentOccupiedVolume = 0,
                AppUserId = addedUser.Id
            };
            Carrier addedCarrier = await _carrierRepository.Insert(carrier);
            await _carrierRepository.Save();
            return _mapper.Map<CarrierDto>(addedCarrier);
        }

        public async Task UpdateCarrier(CarrierDto carrierDto)
        {
            Carrier carrier = _mapper.Map<Carrier>(carrierDto);
            await _carrierRepository.UpdateCarrier(carrier);
        }

        public async Task UpdateCarrierWithUser(UpdateCarrierDto updateCarrierDto)
        {
            Carrier carrier = await _carrierRepository.GetCarrierWithUser(updateCarrierDto.Id);
            AppUser existingUser = await _userManager.FindByNameAsync(updateCarrierDto.UserName);
            if (existingUser != null && existingUser.Id != carrier.AppUserId)
            {
                throw new UsernameAlreadyTakenException();
            }
            carrier.AppUser.UserName = updateCarrierDto.UserName;
            carrier.AppUser.Email = updateCarrierDto.Email;
            await _userManager.UpdateAsync(carrier.AppUser);
            carrier.CompanyName = updateCarrierDto.CompanyName;
            carrier.MaxCargoVolume = updateCarrierDto.MaxCargoVolume;
            carrier.AppUser = null;
            await _carrierRepository.UpdateCarrier(carrier);
        }

        public async Task<bool> DeleteCarrier(Guid id)
        {
            Carrier carrier = await _carrierRepository.GetCarrier(id);
            await _userService.DeleteUser(carrier.AppUserId);
            return true;
        }
    }
}
