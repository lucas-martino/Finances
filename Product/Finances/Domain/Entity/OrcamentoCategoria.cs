namespace Finances.Domain.Entity
{
    public class OrcamentoCategoria : DomainEntity
    {
        public Orcamento Orcamento { get; set; }
        public Categoria Categoria { get; set; }
        public decimal Valor { get; set; }
    }
}