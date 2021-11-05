using StoreAndDeliver.DataLayer.Enums;

namespace StoreAndDeliver.DataLayer.Models
{
    public class Units
    { 
        public WeightUnit Weight { get; set; }
        public LengthUnit Length { get; set; }
        public TemperatureUnit Temperature { get; set; }
        public HumidityUnit Humidity { get; set; }
        public LuminosityUnit Luminosity { get; set; }
    }
}
