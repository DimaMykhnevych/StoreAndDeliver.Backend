using StoreAndDeliver.DataLayer.Models;

namespace StoreAndDeliver.BusinessLayer.DTOs
{
    public class GetRecommendedSettingsDto
    {
        public string CargoDescription { get; set; }
        public Units Units { get; set; }
    }
}
