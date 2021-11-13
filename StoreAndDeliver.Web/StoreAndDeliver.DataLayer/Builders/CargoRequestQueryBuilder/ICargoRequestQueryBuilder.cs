using StoreAndDeliver.DataLayer.Enums;
using StoreAndDeliver.DataLayer.Models;
using System;

namespace StoreAndDeliver.DataLayer.Builders.CargoRequestQueryBuilder
{
    public interface ICargoRequestQueryBuilder : IQueryBuilder<CargoRequest>
    {
        ICargoRequestQueryBuilder SetBaseCargoRequestInfo();
        ICargoRequestQueryBuilder SetCargoRequestType(RequestType? requestType);
        ICargoRequestQueryBuilder SetCargoRequestUser(Guid id);
        ICargoRequestQueryBuilder SetCargoRequestStatus(RequestStatus? requestStatus);
    }
}
