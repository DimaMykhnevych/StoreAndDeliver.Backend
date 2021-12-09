﻿using System;
using System.Collections.Generic;

namespace StoreAndDeliver.BusinessLayer.DTOs
{
    public class RequestsForIotDto
    {
        public IEnumerable<CargoRequestDto> CargoRequests { get; set; }
        public IEnumerable<IoTSettingBoundDto> SettingsBound { get; set; }
    }
}
