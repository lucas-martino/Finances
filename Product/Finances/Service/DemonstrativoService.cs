using Finances.Domain.Entity;
using Finances.Domain.Service;

namespace Finances.Service
{
    public class DemonstrativoService : IFinancesApplicationService
    {
        private DemonstrativoDomainService DemonstrativoDomainService;

        public DemonstrativoService(DemonstrativoDomainService demonstrativoDomainService)
        {
            DemonstrativoDomainService = demonstrativoDomainService;
        }

        public DemonstrativoParcial GetDemonstrativoParcialPorVigencia(Vigencia vigencia)
        {
            return DemonstrativoDomainService.GenereteDemonstrativoParcial(vigencia);
        }

        public Demonstrativo GetDemonstrativoPorVigencia(Vigencia vigencia)
        {
            return DemonstrativoDomainService.GenereteDemonstrativo(vigencia);
        }
    }
}