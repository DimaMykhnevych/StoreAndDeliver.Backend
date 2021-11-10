using StoreAndDeliver.DataLayer.Enums;
using System;

namespace StoreAndDeliver.BusinessLayer.DTOs
{
    public class CargoRequestDto
    {
        public Guid Id { get; set; }
        public RequestStatus Status { get; set; }
        public Guid CargoId { get; set; }
        public Guid RequestId { get; set; }
        public Guid? StoreId { get; set; }

        public StoreDto Store { get; set; }
        public CargoDto Cargo { get; set; }
        public RequestDto Request { get; set; }
    }
}