using StoreAndDeliver.DataLayer.Enums;
using StoreAndDeliver.DataLayer.Models;

namespace StoreAndDeliver.DataLayer.Builders.RequestQueryBuilder
{
    public interface IRequestQueryBuilder : IQueryBuilder<Request>
    {
        IRequestQueryBuilder SetBaseRequestInfo();
        IRequestQueryBuilder SetRequestType(RequestType type);
        IRequestQueryBuilder SortByDeliverByDate();
        IRequestQueryBuilder SetCarryOutBeforeDate();
        IRequestQueryBuilder SetStoreUntilDate();
    }
}
