using Finances.Domain.Entity.Validator;

namespace Finances.Domain.Entity
{
    public class Categoria : FinancesDomainEntity
    {
        public const string DEFAULT_COR = "Black";
        public Categoria()
        {
            Validator = new CategoriaValidator();
            Cor = DEFAULT_COR;
        }

        public string Nome { get; set; }
        public string Cor { get; set; }
        public Usuario Usuario { get; set; }
    }
}