using FluentValidation;

namespace Finances.Domain.Entity
{
    public class Usuario : FinancesDomainEntity
    {
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}