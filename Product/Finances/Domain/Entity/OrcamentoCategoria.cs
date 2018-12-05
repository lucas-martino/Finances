using Finances.Domain.Entity.Validator;

namespace Finances.Domain.Entity
{
    public class OrcamentoCategoria : FinancesDomainEntity
    {
        public OrcamentoCategoria()
        {
            Validator = new OrcamentoCategoriaValidator();
        }

        public Orcamento Orcamento { get; set; }
        public Categoria Categoria { get; set; }
        public decimal Valor { get; set; }
    }
}