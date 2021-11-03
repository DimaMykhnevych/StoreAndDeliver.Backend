using StoreAndDeliver.BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.CityService
{
    public interface ICityService
    {
        IEnumerable<CityDto> GetCities(SearchCityDto searchCityDto);
        Task<CityDto> AddCity(CityDto cityDto);
        Task<bool> DeleteCity(Guid id);
    }
}
