namespace Finances.Domain.Entity
{
    public class OrcamentoCategoria : FinancesDomainEntity
    {
        public Orcamento Orcamento { get; set; }
        public Categoria Categoria { get; set; }
        public decimal Valor { get; set; }
    }
}