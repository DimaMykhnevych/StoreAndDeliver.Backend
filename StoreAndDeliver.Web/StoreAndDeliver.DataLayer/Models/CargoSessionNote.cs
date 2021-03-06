using System;

namespace StoreAndDeliver.DataLayer.Models
{
    public class CargoSessionNote
    {
        public Guid Id { get; set; }
        public DateTime NoteCreationDate { get; set; }
        public string Content { get; set; }

        public Guid CargoSessionId { get; set; }
        public CargoSession CargoSession { get; set; }
    }
}
