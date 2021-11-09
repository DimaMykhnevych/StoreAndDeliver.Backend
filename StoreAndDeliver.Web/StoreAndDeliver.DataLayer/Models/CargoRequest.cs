using System;
using System.Collections.Generic;

namespace StoreAndDeliver.DataLayer.Models
{
    public class CargoRequest
    {
        public Guid Id { get; set; }
        public Guid CargoId { get; set; }
        public Guid RequestId { get; set; }
        public Guid? StoreId { get; set; }

        public Store Store { get; set; }
        public Cargo Cargo { get; set; }
        public Request Request { get; set; }
        public ICollection<CargoSeesion> CargoSeesions { get; set; }

    }
}
