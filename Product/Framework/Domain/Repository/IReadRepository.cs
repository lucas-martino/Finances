using Framework.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Framework.Domain.Repository
{
    public interface IReadRepository<TEntity>
        where TEntity : IEntity
    {
        TEntity GetByID(ulong key);
        Task<TEntity> GetByIDAsync(ulong id);
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();
    }
}