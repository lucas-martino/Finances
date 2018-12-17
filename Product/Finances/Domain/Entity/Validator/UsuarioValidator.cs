using FluentValidation;

namespace Finances.Domain.Entity.Validator
{
    public class UsuarioValidator : FinancesDomainValidator<Usuario>
    {
        public UsuarioValidator()
        {
            RuleFor(u => u.Nome).NotEmpty().MaximumLength(50);
            RuleFor(u => u.Login).NotEmpty().MinimumLength(1).MaximumLength(50);
            RuleFor(u => u.Senha).NotEmpty().MinimumLength(2).MaximumLength(256);
        }
    }
}