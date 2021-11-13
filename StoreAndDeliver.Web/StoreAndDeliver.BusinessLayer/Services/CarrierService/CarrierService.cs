using AutoMapper;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.DataLayer.Models;
using StoreAndDeliver.DataLayer.Repositories.CarrierRepository;
using System;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.CarrierService
{
    public class CarrierService : ICarrierService
    {
        private readonly ICarrierRepository _carrierRepository;
        private readonly IMapper _mapper;

        public CarrierService(ICarrierRepository carrierRepository, IMapper mapper)
        {
            _carrierRepository = carrierRepository;
            _mapper = mapper;
        }

        public async Task<CarrierDto> GetCarrier(Guid id)
        {
            Carrier carrier = await _carrierRepository.GetCarrier(id);
            return _mapper.Map<CarrierDto>(carrier);
        }

        public async Task<CarrierDto> GetCarrierByAppUserId(Guid id)
        {
            Carrier carrier = await _carrierRepository.GetCarrierByAppUserId(id);
            return _mapper.Map<CarrierDto>(carrier);
        }

        public async Task UpdateCarrier(CarrierDto carrierDto)
        {
            Carrier carrier = _mapper.Map<Carrier>(carrierDto);
            await _carrierRepository.UpdateCarrier(carrier);
        }
    }
}
