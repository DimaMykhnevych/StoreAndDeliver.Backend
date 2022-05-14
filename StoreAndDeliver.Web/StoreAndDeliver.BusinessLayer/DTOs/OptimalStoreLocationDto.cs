using System.Collections.Generic;

namespace StoreAndDeliver.BusinessLayer.DTOs
{
    public class OptimalStoreLocationDto
    {
        public double StartLatitude { get; set; }
        public double EndLatitude { get; set; }
        public double StartLongtitude { get; set; }
        public double EndLongtitude { get; set; }
        public IEnumerable<CityDto> Cities { get; set; }
    }
}
