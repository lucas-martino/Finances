using System;
using System.Runtime.Serialization;
using Framework.Domain.Entity.Validator;

namespace Framework.Domain.Entity
{
    [DataContract]
    public abstract class AbastractEntity : IEntity
    {
        protected IEntityValidator<AbastractEntity> Validator { get; set; }

        [DataMember]
        public ulong Id { get; set; }

        public bool Equals(IEntity other)
        {
            return other != null && this.GetType() == other.GetType() && this.Id.Equals(other.Id);
        }

        public static bool operator ==(AbastractEntity left, AbastractEntity right)
        {
            return left != null && left.Equals(right);
        }

        public static bool operator !=(AbastractEntity left, AbastractEntity right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            return obj != null && obj is IEntity && this.Equals(obj as IEntity);
        }

        public override int GetHashCode()
        {
            return 2019061100;
        }

        public bool IsValid()
        {
            ValidationResult result = Validate();

            return result.IsValid;
        }

        public ValidationResult Validate()
        {
            return Validator.Validate(this);
        }

        public bool IsNewEntity()
        {
            return Id <= 0;
        }
    }
}