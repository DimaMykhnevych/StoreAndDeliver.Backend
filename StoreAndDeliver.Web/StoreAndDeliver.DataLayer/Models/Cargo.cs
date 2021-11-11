using System;
using System.Collections.Generic;

namespace StoreAndDeliver.DataLayer.Models
{
    public class Cargo
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public double Weight { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public ICollection<CargoRequest> CargoRequests { get; set; }
        public ICollection<CargoSetting> CargoSettings { get; set; }

        public double GetCargoVolume()
        {
            return (Length * Width * Height) * Amount;
        }

    }
}
