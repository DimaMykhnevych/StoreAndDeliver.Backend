using System;
using System.Collections.Generic;

namespace StoreAndDeliver.DataLayer.Models
{
    public class CargoSession
    {
        public Guid Id { get; set; }
        public Guid CarrierId { get; set; }
        public Guid CargoRequestId { get; set; }

        public Carrier Carrier { get; set; }
        public CargoRequest CargoRequest { get; set; }
        public IEnumerable<CargoSnapshot> CargoSnapshots {get; set;}
        public IEnumerable<CargoSessionNote> CargoSessionNotes { get; set; }
    }
}
