using StoreAndDeliver.DataLayer.Enums;
using StoreAndDeliver.DataLayer.Models;
using System;

namespace StoreAndDeliver.DataLayer.Builders.RequestQueryBuilder
{
    public interface IRequestQueryBuilder : IQueryBuilder<Request>
    {
        IRequestQueryBuilder SetBaseRequestInfo();
        IRequestQueryBuilder SetRequestAddressInfo();
        IRequestQueryBuilder SetRequestId(Guid id);
        IRequestQueryBuilder SetRequestType(RequestType? type);
        IRequestQueryBuilder SortByDeliverByDate();
        IRequestQueryBuilder SetCarryOutBeforeDate(DateTime? time);
        IRequestQueryBuilder SetStoreDates(DateTime? storeFrom, DateTime? storeUntil);
        //IRequestQueryBuilder SetRequestStatus(RequestStatus status);
    }
}
