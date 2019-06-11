using System.Collections.Generic;
using Finances.Domain.Entity.Validator;

namespace Finances.Domain.Entity
{
    public class Orcamento : FinancesEntity
    {
        public Orcamento()
        {
            Validator = new OrcamentoValidator();
            OrcamentosCategoria = new List<OrcamentoCategoria>();
        }

        public Vigencia Vigencia { get; set; }
        public decimal Valor { get; set; }

        public IList<OrcamentoCategoria> OrcamentosCategoria { get; set; }

        public IEnumerable<OrcamentoCategoria> GetOrcamentosCategoria()
        {
            return OrcamentosCategoria;
        }

        public void AddOrcamentoCategoria(OrcamentoCategoria orcamentosCategoria)
        {
            orcamentosCategoria.Orcamento = this;
            OrcamentosCategoria.Add(orcamentosCategoria);
        }
    }
}