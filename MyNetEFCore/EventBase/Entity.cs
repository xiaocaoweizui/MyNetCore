using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyNetEFCore
{ 
    public abstract class Entity : IEntity
    {
        public abstract object[] GetKeys();


        public override string ToString()
        {
            return $"[Entity: {GetType().Name}] Keys = {string.Join(",", GetKeys())}";
        }



        #region 

        /// <summary>
        /// 领域事件
        /// </summary>
        private List<IDomainEvent> _domainEvents;


        /// <summary>
        /// 对外公共领域事件
        /// </summary>
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();


        /// <summary>
        /// 添加领域事件
        /// </summary>
        /// <param name="eventItem"></param>
        public void AddDomainEvent(IDomainEvent eventItem)
        {
            _domainEvents = _domainEvents ?? new List<IDomainEvent>();
            _domainEvents.Add(eventItem);
        }

        /// <summary>
        /// 移除领域事件
        /// </summary>
        /// <param name="eventItem"></param>
        public void RemoveDomainEvent(IDomainEvent eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        /// <summary>
        /// 清楚领域事件
        /// </summary>
        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
        #endregion
    }

    /// <summary>
    ///  实体抽象类，指定类型
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class Entity<TKey> : Entity, IEntity<TKey>
    {
        /// <summary>
        ///  是否请求hashCode
        /// </summary>
        int? _requestedHashCode;

        /// <summary>
        /// 实体的ID
        /// </summary>
        public virtual TKey Id { get; protected set; }

        /// <summary>
        /// 获取实体的key
        /// </summary>
        /// <returns></returns>
        public override object[] GetKeys()
        {
            return new object[] { Id };
        }

        /// <summary>
        /// 实体相等比较
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity<TKey>))
                return false;

            if (Object.ReferenceEquals(this, obj))
                return true;

            if (this.GetType() != obj.GetType())
                return false;

            Entity<TKey> item = (Entity<TKey>)obj;

            if (item.IsTransient() || this.IsTransient())
                return false;
            else
                return item.Id.Equals(this.Id);
        }

        /// <summary>
        /// 获取实体的hashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = this.Id.GetHashCode() ^ 31;

                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();
        }



        //表示对象是否为全新创建的，未持久化的
        public bool IsTransient()
        {
            return EqualityComparer<TKey>.Default.Equals(Id, default);
        }

        /// <summary>
        /// 转换实体成字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"[Entity: {GetType().Name}] Id = {Id}";
        }

        /// <summary>
        /// 实体判断是否相等
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Entity<TKey> left, Entity<TKey> right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null)) ? true : false;
            else
                return left.Equals(right);
        }

        /// <summary>
        /// 实体判断是否不相等
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Entity<TKey> left, Entity<TKey> right)
        {
            return !(left == right);
        }
    }
}
