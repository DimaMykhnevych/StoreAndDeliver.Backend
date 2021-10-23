using System;

namespace StoreAndDeliver.DataLayer.Models
{
    public class CargoSetting
    {
        public Guid Id { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public Guid CargoId { get; set; }
        public Guid EnvironmentSettingId { get; set; }

        public Cargo Cargo { get; set; }
        public EnvironmentSetting EnvironmentSetting { get; set; }
    }
}
