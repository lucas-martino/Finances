namespace Finances.Domain.Entity
{
    public class Categoria : DomainEntity
    {
        public const string DEFAULT_COR = "Black";
        public Categoria()
        {
            Cor = DEFAULT_COR;
        }

        public string Nome { get; set; }
        public string Cor { get; set; }
        public Usuario Usuario { get; set; }
    }
}