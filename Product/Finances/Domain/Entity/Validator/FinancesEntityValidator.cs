using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentValidation;
using Framework.Domain.Entity.Validator;

namespace Finances.Domain.Entity.Validator
{
    public abstract class FinancesEntityValidator<TEntity> : AbstractEntityValidator<TEntity>
        where TEntity : FinancesEntity
    {
    }
}