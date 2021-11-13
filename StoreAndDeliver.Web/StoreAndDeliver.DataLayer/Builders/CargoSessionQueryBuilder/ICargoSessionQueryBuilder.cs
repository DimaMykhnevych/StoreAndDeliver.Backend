using StoreAndDeliver.DataLayer.Enums;
using StoreAndDeliver.DataLayer.Models;
using System;

namespace StoreAndDeliver.DataLayer.Builders.CargoSessionQueryBuilder
{
    public interface ICargoSessionQueryBuilder : IQueryBuilder<CargoSession>
    {
        ICargoSessionQueryBuilder SetBaseCargoSessionInfo();
        ICargoSessionQueryBuilder SetCargoSessionRequestType(RequestType? requestType);
        ICargoSessionQueryBuilder SetCargoSessionCarrier(Guid id);
        ICargoSessionQueryBuilder SetCargoSessionRequestStatus(RequestStatus? requestStatus);

    }
}
