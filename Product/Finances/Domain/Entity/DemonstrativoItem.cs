namespace Finances.Domain.Entity
{
    public class DemonstrativoItem
    {
        public decimal ValorGasto { get; set; }
        public decimal Percentual { get; set; }
        public string Cor { get; set; }
    }

    public class DemonstrativoItemCategoria : DemonstrativoItem
    {
        public Categoria Categoria { get; set; }
    }

    public class DemonstrativoItemOrcamento : DemonstrativoItemCategoria
    {
        public decimal OrcamentoRestante { get; set; }
    }
}