using FluentValidation;

namespace Finances.Domain.Entity.Validator
{
    public class UsuarioValidator : FinancesDomainValidator<Usuario>
    {
        public UsuarioValidator()
        {
            RuleFor(u => u.Nome).NotEmpty();
            RuleFor(u => u.Login).NotEmpty();
            RuleFor(u => u.Senha).NotEmpty();
        }
    }
}