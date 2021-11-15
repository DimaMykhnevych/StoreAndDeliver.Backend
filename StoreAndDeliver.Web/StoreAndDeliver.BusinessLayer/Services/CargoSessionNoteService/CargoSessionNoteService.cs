using AutoMapper;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.DataLayer.Models;
using StoreAndDeliver.DataLayer.Repositories.CargoSessionNoteRepository;
using StoreAndDeliver.DataLayer.Repositories.CargoSessionRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.CargoSessionNoteService
{
    public class CargoSessionNoteService : ICargoSessionNoteService
    {
        private readonly ICargoSessionNoteRepository _cargoSessionNoteRepository;
        private readonly ICargoSessionRepository _cargoSessionRepository;
        private readonly IMapper _mapper;

        public CargoSessionNoteService(ICargoSessionNoteRepository cargoSessionNoteRepository, ICargoSessionRepository cargoSessionRepository, IMapper mapper)
        {
            _cargoSessionNoteRepository = cargoSessionNoteRepository;
            _cargoSessionRepository = cargoSessionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CargoSessionNoteDto>> GetNotesByCargoRequestId(Guid cargoRequestId)
        {
            IEnumerable<CargoSessionNote> cargoSessionNotes = await _cargoSessionNoteRepository.GetNotesByCargoRequestId(cargoRequestId);
            return _mapper.Map<IEnumerable<CargoSessionNoteDto>>(cargoSessionNotes);
        }

        public async Task<CargoSessionNoteDto> AddCargoSessionNote(AddCargoSessionNoteDto addCargoSessionNoteDto)
        {
            CargoSessionNote cargoSessionNote = _mapper.Map<CargoSessionNote>(addCargoSessionNoteDto);
            cargoSessionNote.NoteCreationDate = DateTime.Now;
            cargoSessionNote.Id = Guid.NewGuid();
            cargoSessionNote.CargoSessionId = (await _cargoSessionRepository.GetCargoSessionByCargoRequestId(addCargoSessionNoteDto.CargoRequestId)).Id;
            var added = await _cargoSessionNoteRepository.Insert(cargoSessionNote);
            await _cargoSessionNoteRepository.Save();
            return _mapper.Map<CargoSessionNoteDto>(added);
        }
    }
}
