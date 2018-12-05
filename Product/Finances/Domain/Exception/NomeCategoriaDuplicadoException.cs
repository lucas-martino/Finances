
using Framework.Domain.Entity.Validator;
using Framework.Domain.Exception;

namespace Finances.Domain.Exception
{
    public class NomeCategoriaDuplicadoException : FinancesDomainException
    {
        public NomeCategoriaDuplicadoException(string nome)
            :base(string.Format("Já possui uma categoria com o nome '{0}'.", nome))
        {
        }
    }
}