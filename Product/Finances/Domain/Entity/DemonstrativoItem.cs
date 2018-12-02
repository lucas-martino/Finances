namespace Finances.Domain.Entity
{
    public class DemonstrativoItem
    {
        public Categoria Categoria { get; set; }
        public decimal ValorGasto { get; set; }
        public decimal OrcamentoRestante { get; set; }
        public string Cor { get; set; }
    }

    public class DemonstrativoNaoCategorizado 
    {
        public decimal ValorGasto { get; set; }
        public string Cor { get; set; }
    }
}