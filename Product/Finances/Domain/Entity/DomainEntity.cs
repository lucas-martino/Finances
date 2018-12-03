using System;

namespace Finances.Domain.Entity
{
    public abstract class DomainEntity<TKey> : IEquatable<DomainEntity<TKey>>
        where TKey : IEquatable<TKey>
    {
        public TKey ID { get; set; }
        protected IValidator<DomainEntity<TKey>> Validator { get; set; }

        public bool Equals(DomainEntity<TKey> other)
        {
            return other != null && this.GetType() == other.GetType() && this.ID.Equals(other.ID);
        }
/* 
        public override bool Equals(object obj)
        {
            return obj != null && obj is DomainEntity<TKey> && this.Equals(obj as DomainEntity<TKey>);
        }

        public static bool operator ==(DomainEntity<TKey> left, DomainEntity<TKey> right)
        {

            return left.Equals(right);
        }

        public static bool operator !=(DomainEntity<TKey> left, DomainEntity<TKey> right)
        {
            return !(left == right);
        }
*/

        public bool IsValid()
        {
            ValidationResult result = Validate();

            return result.IsValid;
        }

        public ValidationResult Validate()
        {
            return Validator.Validate(this);
        }
    }
}