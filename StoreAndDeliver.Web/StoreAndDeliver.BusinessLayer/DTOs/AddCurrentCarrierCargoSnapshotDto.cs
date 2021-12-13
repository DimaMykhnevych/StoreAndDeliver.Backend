using StoreAndDeliver.DataLayer.Enums;

namespace StoreAndDeliver.BusinessLayer.DTOs
{
    public class AddCurrentCarrierCargoSnapshotDto
    {
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double Luminosity { get; set; }
        public RequestType RequestType { get; set; }
    }
}
