using System;
using Finances.Domain.Entity.Validator;

namespace Finances.Domain.Entity
{
    public class Gasto : FinancesEntity
    {
        public Gasto()
        {
            Validator = new GastoValidator();
        }

        public Vigencia Vigencia { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public Categoria Categoria { get; set; }
        public string Observacao { get; set; }
    }
}