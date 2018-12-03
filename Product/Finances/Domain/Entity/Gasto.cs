using System;

namespace Finances.Domain.Entity
{
    public class Gasto : FinancesDomainEntity
    {
        public Vigencia Vigencia { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public Categoria Categoria { get; set; }
        public string Observacao { get; set; }
    }
}