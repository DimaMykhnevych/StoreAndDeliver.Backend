using System;

namespace StoreAndDeliver.DataLayer.Models
{
    public class Store
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double MaxCargoVolume { get; set; }
        public double CurrentOccupiedVolume { get; set; }
        public Guid AddressId { get; set; }
        public Address Address { get; set; }
    }
}
