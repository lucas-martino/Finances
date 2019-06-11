using Framework.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Framework.Domain.Repository
{
    public interface ICRUDRepository<TEntity> : IReadRepository<TEntity>
        where TEntity : IEntity
    {
        ulong Save(TEntity entity);
        void Delete(TEntity entity);
        void Delete(ulong id);
    }
}