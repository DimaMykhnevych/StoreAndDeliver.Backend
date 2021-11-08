using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.DataLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.CargoService
{
    public interface ICargoService
    {
        Task<CargoDto> AddCargo(AddCargoDto addCargoDto, Units units);
        Task<IEnumerable<CargoDto>> AddCargoRange(IEnumerable<AddCargoDto> addCargoDtos, Units units);
    }
}
