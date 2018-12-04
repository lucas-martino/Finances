using System.Collections.Generic;
using Framework.Domain.Entity;

namespace Finances.Domain.Entity
{
    public class Demonstrativo : DemonstrativoParcial
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
}