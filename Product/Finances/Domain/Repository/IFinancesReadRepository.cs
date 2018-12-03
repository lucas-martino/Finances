using Finances.Domain.Entity;
using System.Collections.Generic;

namespace Finances.Domain.Repository
{
    public interface IFinancesReadRepository<TEntiy> : IReadRepository<TEntiy, int>
        where TEntiy : FinancesDomainEntity
    {
    }
}