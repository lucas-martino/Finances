using FluentValidation;

namespace Finances.Domain.Entity.Validator
{
    public class CategoriaValidator : FinancesDomainValidator<Categoria>
    {
        public CategoriaValidator()
        {
            RuleFor(v => v.Usuario).NotNull().SetValidator(new UsuarioValidator());
            RuleFor(v => v.Nome).NotEmpty();
            RuleFor(v => v.Cor).NotEmpty();
        }
    }
}