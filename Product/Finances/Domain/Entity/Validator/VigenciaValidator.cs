using FluentValidation;

namespace Finances.Domain.Entity.Validator
{
    public class VigenciaValidator : FinancesEntityValidator<Vigencia>
    {
        public VigenciaValidator()
        {
            RuleFor(v => v.Usuario).NotNull().SetValidator(new UsuarioValidator());
            RuleFor(v => v.Referencia).GreaterThan(201800).LessThan(210000);
        }
    }
}