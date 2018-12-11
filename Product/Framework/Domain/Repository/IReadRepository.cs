using Framework.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Framework.Domain.Repository
{
    public interface IReadRepository<TEntity, TKey>
        where TEntity : DomainEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        TEntity GetByID(TKey key);
        Task<TEntity> GetByIDAsync(TKey id);
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();
    }
}