using System;

namespace StoreAndDeliver.BusinessLayer.DTOs
{
    public class UpdateCarrierDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public double MaxCargoVolume { get; set; }
    }
}
