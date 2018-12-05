using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentValidation;
using Framework.Domain.Entity.Validator;

namespace Finances.Domain.Entity.Validator
{
    public abstract class FinancesDomainValidator<TEntity> : DomainValidator<TEntity>
        where TEntity : FinancesDomainEntity
    {
    }
}