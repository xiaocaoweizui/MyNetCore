using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyNetEFCore
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        IUnitOfWork UnitOfWork { get; }
        TEntity Add(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        TEntity Update(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        bool Remove(Entity entity);
        Task<bool> RemoveAsync(Entity entity);
    }


    public interface IRepository<TEntity, TKey> : IRepository<TEntity> where TEntity : Entity<TKey>
    {
        bool Delete(TKey id);
        Task<bool> DeleteAsync(TKey id, CancellationToken cancellationToken = default);
        TEntity Get(TKey id);
        Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken = default);
    }
}
