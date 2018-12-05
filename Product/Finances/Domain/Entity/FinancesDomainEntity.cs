using Framework.Domain.Entity;

namespace Finances.Domain.Entity
{
    public abstract class FinancesDomainEntity : DomainEntity<int>
    { 
        public override bool IsNewEntity()
        {
            return ID <= 0;
        }
    }
}