using Framework.Domain.Entity;
using System;
using System.Collections.Generic;

namespace Framework.Domain.Repository
{
    public interface ICRUDRepository<TEntity, TKey> : IReadRepository<TEntity, TKey>
        where TEntity : DomainEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        TKey Save(TEntity entity);
        void Delete(TEntity entity);
        void Delete(TKey id);
    }
}