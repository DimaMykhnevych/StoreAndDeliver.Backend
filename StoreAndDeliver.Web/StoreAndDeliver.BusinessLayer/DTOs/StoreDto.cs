using System;
using System.Collections.Generic;

namespace StoreAndDeliver.BusinessLayer.DTOs
{
    public class StoreDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double MaxCargoVolume { get; set; }
        public double CurrentOccupiedVolume { get; set; }
        public Guid AddressId { get; set; }
        public AddressDto Address { get; set; }

        public ICollection<CargoRequestDto> CargoRequests { get; set; }
    }
}