using System.Collections.Generic;

namespace Finances.Domain.Entity
{
    public class Demonstrativo : IValueObject
    {
        public Demonstrativo()
        {
            DemaisCategorias = new List<DemonstrativoItem>();
            PercentualDistibuicao = new List<DemonstrativoParcela>();
        }

        public decimal PercentualOrcamento { get; set; }
        public decimal Orcamento { get; set; }
        public IList<DemonstrativoItem> DemaisCategorias { get; private set; }
        public IList<DemonstrativoParcela> PercentualDistibuicao { get; private set; }
    }

    public class DemonstrativoParcial : IValueObject
    {
        public DemonstrativoParcial()
        {
            Orcamentos = new List<DemonstrativoItem>();
        }

        public decimal ValorGastoTotal { get; set; }        
        public string Cor { get; set; }

        public IList<DemonstrativoItem> Orcamentos { get; set; }
        public DemonstrativoNaoCategorizado NaoCategorizado { get; set; }
    }
}