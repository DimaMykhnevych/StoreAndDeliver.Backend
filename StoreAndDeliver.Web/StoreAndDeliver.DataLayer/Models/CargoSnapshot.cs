using System;

namespace StoreAndDeliver.DataLayer.Models
{
    public class CargoSnapshot
    {
        public Guid Id { get; set; }
        public Guid CargoSessionId { get; set; }
        public Guid EnvironmentSettingId { get; set; }
        public double Value { get; set; }

        public CargoSession CargoSession { get; set; }
        public EnvironmentSetting EnvironmentSetting { get; set; }
    }
}
