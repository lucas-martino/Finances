using System;
using System.Linq.Expressions;

namespace Framework.Domain.Entity.Validator
{
    public interface IEntityValidator<out TEntity>
        where TEntity : IEntity
    {
        ValidationResult Validate(IEntity instance);
    }
}