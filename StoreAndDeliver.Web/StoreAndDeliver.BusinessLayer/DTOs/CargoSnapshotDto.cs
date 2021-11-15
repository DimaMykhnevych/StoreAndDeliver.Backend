using System;

namespace StoreAndDeliver.BusinessLayer.DTOs
{
    public class CargoSnapshotDto
    {
        public Guid Id { get; set; }
        public Guid CargoSessionId { get; set; }
        public Guid EnvironmentSettingId { get; set; }
        public double Value { get; set; }

        public CargoSessionDto CargoSession { get; set; }
        public EnvironmentSettingDto EnvironmentSetting { get; set; }
    }
}
