using System;

namespace StoreAndDeliver.BusinessLayer.DTOs
{
    public class AddCargoSnapshotDto
    {
        public Guid Id { get; set; }
        public Guid CargoSessionId { get; set; }
        public Guid EnvironmentSettingId { get; set; }
        public double Value { get; set; }
    }
}
