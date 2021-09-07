using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyNetEFCore
{
    /// <summary>
    /// 仓储基础抽象类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDbContext"></typeparam>
    public abstract class Repository<TEntity, TDbContext> : IRepository<TEntity> where TEntity : Entity where TDbContext : EFContext
    {
        /// <summary>
        /// 对象上下文
        /// </summary>
        protected virtual TDbContext DbContext { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="context"></param>
        public Repository(TDbContext context)
        {
            this.DbContext = context;
        }

        /// <summary>
        /// 获取当前工作单元
        /// </summary>
        public virtual IUnitOfWork UnitOfWork => DbContext;


        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual TEntity Add(TEntity entity)
        {
            return DbContext.Add(entity).Entity;
        }

        /// <summary>
        /// 异步新增
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Add(entity));
        }

        public virtual TEntity Update(TEntity entity)
        {
            return DbContext.Update(entity).Entity;
        }

        public virtual Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Update(entity));
        }

        public virtual bool Remove(Entity entity)
        {
            DbContext.Remove(entity);
            return true;
        }

        public virtual Task<bool> RemoveAsync(Entity entity)
        {
            return Task.FromResult(Remove(entity));
        }
    }


    public abstract class Repository<TEntity, TKey, TDbContext> : Repository<TEntity, TDbContext>, IRepository<TEntity, TKey> where TEntity : Entity<TKey> where TDbContext : EFContext
    {
        public Repository(TDbContext context) : base(context)
        {
        }

        public virtual bool Delete(TKey id)
        {
            var entity = DbContext.Find<TEntity>(id);
            if (entity == null)
            {
                return false;
            }
            DbContext.Remove(entity);
            return true;
        }

        public virtual async Task<bool> DeleteAsync(TKey id, CancellationToken cancellationToken = default)
        {
            var entity = await DbContext.FindAsync<TEntity>(id, cancellationToken);
            if (entity == null)
            {
                return false;
            }
            DbContext.Remove(entity);
            return true;
        }

        public virtual TEntity Get(TKey id)
        {
            return DbContext.Find<TEntity>(id);
        }

        public virtual async Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken = default)
        {
            return await DbContext.FindAsync<TEntity>(id, cancellationToken);
        }
    }



}
