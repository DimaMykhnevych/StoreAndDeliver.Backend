using System;

namespace StoreAndDeliver.BusinessLayer.DTOs
{
    public class CityDto
    {
        public Guid Id { get; set; }
        public string Country { get; set; }
        public string CityName { get; set; }
        public double Longtitude { get; set; }
        public double Latitude { get; set; }
    }
}
