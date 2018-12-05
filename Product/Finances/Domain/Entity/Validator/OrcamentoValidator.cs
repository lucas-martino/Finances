using FluentValidation;

namespace Finances.Domain.Entity.Validator
{
    public class OrcamentoValidator : FinancesDomainValidator<Orcamento>
    {
        public OrcamentoValidator()
        {
            RuleFor(o => o.Vigencia).NotNull().SetValidator(new VigenciaValidator());
            RuleFor(o => o.Valor).GreaterThanOrEqualTo(0);
        }
    }
}