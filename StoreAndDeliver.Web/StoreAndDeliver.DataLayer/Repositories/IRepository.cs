using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAndDeliver.DataLayer.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Get(Guid id);
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Insert(TEntity entity);
        void Delete(TEntity entity);
        Task Update(TEntity entity);
        Task Save();
    }
}
