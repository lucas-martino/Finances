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

        public IList<DemonstrativoItem> DemaisCategorias { get; set; }
        public IList<DemonstrativoParcela> PercentualDistibuicao { get; set; }
    }
}