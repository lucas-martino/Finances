using FluentValidation;

namespace Finances.Domain.Entity.Validator
{
    public class CategoriaValidator : FinancesDomainValidator<Categoria>
    {
        public CategoriaValidator()
        {
            RuleFor(u => u.Usuario).NotNull().SetValidator(new UsuarioValidator());
            RuleFor(u => u.Nome).NotEmpty().MinimumLength(3).MaximumLength(15);
            RuleFor(u => u.Cor).NotEmpty().MaximumLength(50);
            RuleFor(u => u.Icone).MaximumLength(50);
            RuleFor(u => u.Pai).Custom((categoria, context)=> { 
                if (categoria != null)
                {
                    if (!categoria.PermiteFilhos())
                        context.AddFailure("Level 2+ não pode ter filhos");
                    else 
                    {
                        if (!categoria.Validate().IsValid)
                            context.AddFailure("Categoria Pai inválida.");
                    }
                }
            });
        }        
    }
}