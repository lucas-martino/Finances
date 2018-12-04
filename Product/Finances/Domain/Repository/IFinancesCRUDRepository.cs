using Finances.Domain.Entity;
using System.Collections.Generic;
using Framework.Domain.Repository;

namespace Finances.Domain.Repository
{
    public interface IFinancesCRUDRepository<TEntity> : ICRUDRepository<TEntity, int>
        where TEntity : FinancesDomainEntity
    {

    }
}