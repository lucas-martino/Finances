using FluentValidation;

namespace Finances.Domain.Entity.Validator
{
    public class OrcamentoCategoriaValidator : FinancesEntityValidator<OrcamentoCategoria>
    {
        public OrcamentoCategoriaValidator()
        {
            RuleFor(o => o.Orcamento).NotNull();
            RuleFor(o => o.Categoria).NotNull().SetValidator(new CategoriaValidator());
            RuleFor(o => o.Valor).GreaterThan(0);
        }
    }
}