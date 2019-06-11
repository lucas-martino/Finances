
using System.Text;
using Framework.Domain.Entity;
using Framework.Domain.Entity.Validator;

namespace Framework.Domain.Exception
{
    public class EntityInvalidException : DomainException
    {
        private ValidationResult ValidationResult;
        public EntityInvalidException(IEntity entity, ValidationResult validationResult)
            :base(GenereteMessage(entity, validationResult))
        {
            ValidationResult = validationResult;
        }

        private static string GenereteMessage(IEntity entity, ValidationResult validationResult)
        {
            StringBuilder errorMessage = new StringBuilder();

            errorMessage.AppendFormat("Entidade '{0}' inválida. ", entity.ToString());
            foreach (var item in validationResult.Errors)
                errorMessage.AppendFormat("{0}; ", item.ErrorMessage);

            return errorMessage.ToString();
        }
    }
}