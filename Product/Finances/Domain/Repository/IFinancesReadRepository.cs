using Finances.Domain.Entity;
using System.Collections.Generic;
using Framework.Domain.Repository;

namespace Finances.Domain.Repository
{
    public interface IFinancesReadRepository<TEntiy> : IReadRepository<TEntiy, int>
        where TEntiy : FinancesDomainEntity
    {
    }
}