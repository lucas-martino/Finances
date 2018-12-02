namespace Finances.Domain.Entity
{
    public class Usuario : DomainEntity
    {
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}