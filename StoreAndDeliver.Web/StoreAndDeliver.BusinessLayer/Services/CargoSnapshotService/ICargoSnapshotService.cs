using StoreAndDeliver.BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.CargoSnapshotService
{
    public interface ICargoSnapshotService
    {
        Task<IEnumerable<GetUserCargoSnapshotsDto>> GetUserCargoSnapshots(Guid userId, GetCargoSnapshotDto getCargoSnapshotDto);
        Task<IEnumerable<CargoSnapshotDto>> GetCargoSnapshotsByCargoRequestId(GetCargoSnapshotDto getCargoSnapshotDto);
        Task<IEnumerable<CargoSnapshotDto>> GetCargoSnapshotsByCargoSessionId(Guid cargoSessionId);
        Task<CargoSnapshotDto> AddCargoSnapshot(AddCargoSnapshotDto addCargoSnapshotDto);
    }
}
