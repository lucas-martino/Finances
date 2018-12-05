using System;
using System.Linq.Expressions;

namespace Framework.Domain.Entity.Validator
{
    public interface IDomainValidator<out TEntity>
        where TEntity : DomainEntity
    {
        ValidationResult Validate(DomainEntity instance);
    }
}