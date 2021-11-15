using System;

namespace StoreAndDeliver.BusinessLayer.DTOs
{
    public class AddCargoSessionNoteDto
    {
        public Guid Id { get; set; }
        public DateTime NoteCreationDate { get; set; }
        public string Content { get; set; }
        public Guid CargoRequestId { get; set; }
    }
}
