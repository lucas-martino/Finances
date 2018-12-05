
using Framework.Domain.Entity.Validator;
using Framework.Domain.Exception;

namespace Finances.Domain.Exception
{
    public class CategoriaComLancemntosException : FinancesDomainException
    {
        public CategoriaComLancemntosException()
            :base(string.Format("Não é possivel apagar uma categoria que já possui lançamentos"))
        {
        }
    }
}