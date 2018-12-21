using System.Collections.Generic;
using Framework.Domain.Entity;

namespace Finances.Domain.Entity
{
    public class DemonstrativoParcial : IValueObject
    {
        public DemonstrativoParcial()
        {
            Orcamentos = new List<DemonstrativoItemOrcamento>();
        }

        public decimal ValorGastoTotal { get; set; }
        public decimal PercentualOrcamento { get; set; }
        public decimal OrcamentoRestante { get; set; }
        public decimal OrcamentoPlanejado { get; set; }   
        public string Cor { get; set; }

        public IList<DemonstrativoItemOrcamento> Orcamentos { get; set; }
        public DemonstrativoItem NaoCategorizado { get; set; }
    }
}