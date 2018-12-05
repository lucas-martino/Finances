using System;
using FluentValidation;

namespace Finances.Domain.Entity.Validator
{
    public class GastoValidator : FinancesDomainValidator<Gasto>
    {
        public GastoValidator()
        {
            RuleFor(g => g.Vigencia).NotNull().SetValidator(new VigenciaValidator());
            RuleFor(g => g.Valor).GreaterThan(0);
            RuleFor(g => g.Categoria).SetValidator(new CategoriaValidator());
        }
    }
}