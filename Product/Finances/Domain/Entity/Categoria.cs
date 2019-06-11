using System;
using System.Runtime.Serialization;
using Finances.Domain.Entity.Validator;

namespace Finances.Domain.Entity
{
    [DataContract]
    public class Categoria : FinancesEntity
    {
        public const string DEFAULT_COR = "Darkgray";
        public Categoria()
        {
            Validator = new CategoriaValidator();
            Cor = DEFAULT_COR;
        }

        [DataMember]
        public string Nome { get; set; }
        [DataMember]
        public string Cor { get; set; }
        [DataMember]
        public string Icone { get; set; }
        [DataMember]
        public Categoria Pai { get; set; }
        public Usuario Usuario { get; set; }

        public bool PermiteFilhos()
        {
            return Pai == null;
        }
    }
}