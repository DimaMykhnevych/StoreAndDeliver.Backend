using System;

namespace StoreAndDeliver.BusinessLayer.DTOs
{
    public class CargoSessionDto
    {
        public Guid Id { get; set; }
        public Guid CarrierId { get; set; }
        public Guid CargoRequestId { get; set; }

        public CarrierDto Carrier { get; set; }
        public CargoRequestDto CargoRequest { get; set; }
    }
}
