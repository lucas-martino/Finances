using System;
using Framework.Domain.Entity.Validator;

namespace Framework.Domain.Entity
{
    public abstract class DomainEntity<TKey> : IEquatable<DomainEntity<TKey>>
        where TKey : IEquatable<TKey>
    {
        public TKey ID { get; set; }
        

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
            return left != null && left.Equals(right);
        }

        public static bool operator !=(DomainEntity<TKey> left, DomainEntity<TKey> right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return 2018120400;
        }
*/

        protected IDomainValidator<DomainEntity<TKey>, TKey> Validator { get; set; }
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