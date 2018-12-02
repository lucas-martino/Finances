using Finances.Domain.Entity;
using System.Collections.Generic;

namespace Finances.Domain.Repository
{
    public interface ICRUDRepository<TEntity> : IReadRepository<TEntity>
        where TEntity : DomainEntity
    {
        int Save(TEntity entity);
        void Delete(TEntity entity);
        void Delete(int id);
    }
}