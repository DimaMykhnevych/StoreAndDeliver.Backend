using StoreAndDeliver.DataLayer.Models;
using System;
using System.Collections.Generic;

namespace StoreAndDeliver.BusinessLayer.DTOs
{
    public class UpdateCargoRequestsDto
    {
        public Dictionary<Guid, List<CargoRequest>> RequestGroup { get; set; }
        public Units Units { get; set; }
        public string Language { get; set; }
    }
}
