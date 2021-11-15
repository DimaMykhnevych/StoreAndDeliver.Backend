using StoreAndDeliver.DataLayer.Enums;
using System;

namespace StoreAndDeliver.BusinessLayer.DTOs
{
    public class GetCargoSnapshotDto
    {
        public Guid CargoRequestId { get; set; }
        public TemperatureUnit Temperature { get; set; }
        public HumidityUnit Humidity { get; set; }
        public LuminosityUnit Luminosity { get; set; }
    }
}
