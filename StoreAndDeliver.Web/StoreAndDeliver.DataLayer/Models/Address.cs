using System;
using System.Collections.Generic;

namespace StoreAndDeliver.DataLayer.Models
{
    public class Address
    {
        public Guid Id { get; set; }
        public double Longtitude { get; set; }
        public double Latitude { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

        public ICollection<Request> RequestsFrom { get; set; }
        public ICollection<Request> RequestsTo { get; set; }
        public Store Store { get; set; }
    }
}
