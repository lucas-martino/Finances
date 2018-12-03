using Finances.Domain.Entity;
using System.Collections.Generic;

namespace Finances.Domain.Repository
{
    public interface IFinancesCRUDRepository<TEntity> : ICRUDRepository<TEntity, int>
        where TEntity : FinancesDomainEntity
    {

    }
}