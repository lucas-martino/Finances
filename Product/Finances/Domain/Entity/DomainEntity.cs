namespace Finances.Domain.Entity
{
    public abstract class DomainEntity : DomainEntity<int>
    {
    }

    public abstract class DomainEntity<TKey>
    {
        public TKey ID { get; set; }
    }
}