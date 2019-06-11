using Finances.Domain.Entity;
using Framework.Domain.Repository.EF;

namespace Finances.Domain.Repository.EF
{
    public abstract class FinancesCRUDRepository<TEntity> : AbastracCRUDRepository<TEntity, FinancesContext>
        where TEntity: FinancesEntity
    {
        public FinancesCRUDRepository(FinancesContext dbContext)
            : base(dbContext)
        {
        }
    }
}