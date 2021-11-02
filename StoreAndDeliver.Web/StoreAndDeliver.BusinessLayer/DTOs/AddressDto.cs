using System;

namespace StoreAndDeliver.BusinessLayer.DTOs
{
    public class AddressDto
    {
        public Guid Id { get; set; }
        public double Longtitude { get; set; }
        public double Latitude { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
    }
}
