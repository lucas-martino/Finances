using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentValidation;

namespace Framework.Domain.Entity.Validator
{
    public abstract class DomainValidator<TEntity> : AbstractValidator<TEntity>, IDomainValidator<TEntity>
        where TEntity : DomainEntity
    {
        public ValidationResult Validate(DomainEntity instance)
        {
            return Convert(base.Validate(instance as TEntity));
        }

        private static ValidationResult Convert(FluentValidation.Results.ValidationResult result)
        {
            return new ValidationResult(Convert(result.Errors));
        }

        private static IList<ValidationFailure> Convert(IList<FluentValidation.Results.ValidationFailure> result)
        {
            if (result is null)
                return null;
            else 
            {
                IList<ValidationFailure> lista = new List<ValidationFailure>();
                foreach (var item in result)
                    lista.Add(Convert(item));

                return lista;
            }
        }

        private static ValidationFailure Convert(FluentValidation.Results.ValidationFailure result)
        {
            ValidationFailure vf = new ValidationFailure();
            vf.ErrorMessage = result.ErrorMessage;
            vf.PropertyName = result.PropertyName;
            vf.Severity = (Severity)result.Severity;

            return vf;
        }
    }
}