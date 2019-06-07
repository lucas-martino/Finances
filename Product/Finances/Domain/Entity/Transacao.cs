using System;
using System.Runtime.Serialization;

namespace Finances.Domain.Entity
{
    [DataContract]
    public class Transacao : FinancesDomainEntity
    {
        public Transacao(TipoTransacao tipo)
        {
            this.Tipo = tipo;
        }

        [DataMember]
        public TipoTransacao Tipo { get; set; }
        [DataMember]
        public DateTime Data { get; set; }
        [DataMember]
        public decimal Valor { get; set; }
        [DataMember]
        public string Descricao { get; set; }
        [DataMember]
        public Categoria Categoria { get; set; }
        public Usuario Usuario { get; set; }
    }
}