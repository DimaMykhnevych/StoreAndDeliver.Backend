using System.Linq;

namespace StoreAndDeliver.DataLayer.Builders
{
    public interface IQueryBuilder<TEntity>
    {
        IQueryable<TEntity> Build();
    }
}
