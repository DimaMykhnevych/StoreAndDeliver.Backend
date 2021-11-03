using System;
using System.Collections.Generic;

namespace StoreAndDeliver.BusinessLayer.DTOs
{
    public class AddRequestDto
    {
        public Guid CurrentUserId { get; set; }
        public RequestDto Request { get; set; }
        public IEnumerable<AddCargoDto> Cargo { get; set; }
        public string CurrentLanguage { get; set; }
    }
}
