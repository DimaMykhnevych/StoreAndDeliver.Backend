using StoreAndDeliver.DataLayer.Models;
using System;
using System.Collections.Generic;

namespace StoreAndDeliver.BusinessLayer.DTOs
{
    public class CarrierDto
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public double MaxCargoVolume { get; set; }
        public double CurrentOccupiedVolume { get; set; }

        public Guid AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
        public ICollection<CargoSessionDto> CargoSeesions { get; set; }
    }
}
