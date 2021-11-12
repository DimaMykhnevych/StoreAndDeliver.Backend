﻿using StoreAndDeliver.DataLayer.Enums;
using StoreAndDeliver.DataLayer.Models;

namespace StoreAndDeliver.BusinessLayer.DTOs
{
    public class GetOptimizedRequestDto
    {
        public RequestType RequestType { get; set; }
        public Units Units { get; set; }
        public string CurrentLanguage { get; set; }
    }
}
