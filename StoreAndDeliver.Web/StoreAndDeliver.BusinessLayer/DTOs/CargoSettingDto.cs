using System;

namespace StoreAndDeliver.BusinessLayer.DTOs
{
    public class CargoSettingDto
    {
        public Guid Id { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public Guid CargoId { get; set; }
        public Guid EnvironmentSettingId { get; set; }
    }
}
