using StoreAndDeliver.BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.CargoSessionNoteService
{
    public interface ICargoSessionNoteService
    {
        Task<IEnumerable<CargoSessionNoteDto>> GetNotesByCargoRequestId(Guid cargoRequestId);
        Task<CargoSessionNoteDto> AddCargoSessionNote(AddCargoSessionNoteDto addCargoSessionNoteDto);
    }
}
