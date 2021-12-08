using System;
using System.Collections.Generic;

namespace StoreAndDeliver.BusinessLayer.DTOs
{
    public class RequestsForIotDto
    {
        public IEnumerable<CargoRequestDto> CargoRequests { get; set; }
        public Dictionary<string, SettingsBoundDto> SettingsBound { get; set; }
    }
}
