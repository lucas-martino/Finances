using Finances.Domain.Entity;
using Framework.Domain.Repository.EF;

namespace Finances.Domain.Repository.EF
{
    public abstract class FinancesReadRepository<TEntity> : AbstractReadRepository<TEntity, FinancesContext>
        where TEntity: FinancesEntity
    {
        public FinancesReadRepository(FinancesContext dbContext)
            : base(dbContext)
        {
        }
    }
}