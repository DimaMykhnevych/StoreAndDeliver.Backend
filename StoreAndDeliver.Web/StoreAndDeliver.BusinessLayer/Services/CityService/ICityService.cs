using StoreAndDeliver.BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.CityService
{
    public interface ICityService
    {
        Task<IEnumerable<CityDto>> GetCities();
        Task<CityDto> AddCity(CityDto cityDto);
        Task<bool> DeleteCity(Guid id);
    }
}
