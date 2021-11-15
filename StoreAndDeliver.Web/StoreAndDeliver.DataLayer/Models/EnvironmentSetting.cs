using System;
using System.Collections.Generic;

namespace StoreAndDeliver.DataLayer.Models
{
    public class EnvironmentSetting
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<CargoSetting> CargoSettings { get; set; }
        public IEnumerable<CargoSnapshot> CargoSnapshots { get; set; }

    }
}
