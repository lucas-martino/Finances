using System;
using System.Linq.Expressions;

namespace Framework.Domain.Entity.Validator
{
    public interface IDomainValidator<out TEntity, TKey>
        where TEntity : DomainEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        ValidationResult Validate(DomainEntity<TKey> instance);
    }
}