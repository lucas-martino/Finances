using System;
using System.Linq.Expressions;

namespace Finances.Domain.Entity
{
    public interface IValidator<T>
    {
        ValidationResult Validate(T instance);
    }
}