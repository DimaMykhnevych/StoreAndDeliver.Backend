using System;


namespace StoreAndDeliver.BusinessLayer.DTOs
{
    public class CargoRequestIoTDto
    {
        public Guid Id { get; set; }

        public CargoIoTDto Cargo { get; set; }
    }
}
