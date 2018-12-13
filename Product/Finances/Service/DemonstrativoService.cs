using Finances.Domain.Entity;
using Finances.Domain.Service;

namespace Finances.Service
{
    public class DemonstrativoService : IFinancesApplicationService
    {
        private DemonstrativoDomainService DemonstrativoDomainService;
        private VigenciaService VigenciaService;

        public DemonstrativoService(DemonstrativoDomainService demonstrativoDomainService, VigenciaService vigenciaService)
        {
            DemonstrativoDomainService = demonstrativoDomainService;
            VigenciaService = vigenciaService;
        }

        public DemonstrativoParcial GetDemonstrativoParcialVigenciaAtual(int usuarioID)
        {
            Vigencia vigenciaAtual = VigenciaService.GetVigenciaAtualPorUsuario(usuarioID);
            return DemonstrativoDomainService.GenereteDemonstrativoParcial(vigenciaAtual);
        }

        public Demonstrativo GetDemonstrativoVigenciaAtual(int usuarioID)
        {
            Vigencia vigenciaAtual = VigenciaService.GetVigenciaAtualPorUsuario(usuarioID);
            return DemonstrativoDomainService.GenereteDemonstrativo(vigenciaAtual);
        }
    }
}