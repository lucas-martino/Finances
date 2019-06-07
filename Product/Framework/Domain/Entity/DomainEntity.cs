using System;
using System.Runtime.Serialization;
using Framework.Domain.Entity.Validator;

namespace Framework.Domain.Entity
{
    [DataContract]
    public abstract class DomainEntity<TKey> : DomainEntity, IEquatable<DomainEntity<TKey>>
        where TKey : IEquatable<TKey>
    {
        [DataMember]
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
    }

    public abstract class DomainEntity
    {
        protected IDomainValidator<DomainEntity> Validator { get; set; }
        public bool IsValid()
        {
            ValidationResult result = Validate();

            return result.IsValid;
        }

        public ValidationResult Validate()
        {
            return Validator.Validate(this);
        }

        public abstract bool IsNewEntity();
    }
}