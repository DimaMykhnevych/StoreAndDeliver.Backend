using System;

namespace StoreAndDeliver.DataLayer.Models
{
    public class Shipping
    {
        public Guid Id { get; set; }
        public Guid CarrierId { get; set; }
        public Guid CargoRequestId { get; set; }

        public Carrier Carrier { get; set; }
        public CargoRequest CargoRequest { get; set; }
    }
}
