using System.Collections.Generic;

namespace StoreAndDeliver.BusinessLayer.DTOs
{
    public class GetUserCargoSnapshotsDto
    {
        public CargoDto Cargo { get; set; }
        public IEnumerable<CargoSnapshotDto> CargoSnapshots { get; set; }
    }
}
