using AutoMapper;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.DataLayer.Models;
using StoreAndDeliver.DataLayer.Repositories.CityRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.CityService
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public CityService(ICityRepository cityRepository, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CityDto>> GetCities()
        {
            IEnumerable<City> cities = await _cityRepository.GetAll();
            return _mapper.Map<IEnumerable<CityDto>>(cities);
        }

        public async Task<CityDto> AddCity(CityDto cityDto)
        {
            City city = _mapper.Map<City>(cityDto);
            city.Id = new Guid();
            var addedCity = await _cityRepository.Insert(city);
            await _cityRepository.Save();
            return _mapper.Map<CityDto>(addedCity);
        }

        public async Task<bool> DeleteCity(Guid id)
        {
            City cityToDelete = await _cityRepository.Get(id);
            if (cityToDelete == null)
            {
                return false;
            }
            _cityRepository.Delete(cityToDelete);
            await _cityRepository.Save();
            return true;
        }
    }
}
