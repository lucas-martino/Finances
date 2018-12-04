using System.Collections.Generic;
using Framework.Domain.Entity;

namespace Finances.Domain.Entity
{
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