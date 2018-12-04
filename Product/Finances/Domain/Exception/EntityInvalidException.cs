
using Framework.Domain.Entity.Validator;
using Framework.Domain.Exception;

namespace Finances.Domain.Exception
{
    public class EntityInvalidException : DomainException
    {
        private ValidationResult ValidationResult;
        public EntityInvalidException(ValidationResult validationResult)
            :base("Entidade inválida.")
        {
            ValidationResult = validationResult;
        }
    }
}