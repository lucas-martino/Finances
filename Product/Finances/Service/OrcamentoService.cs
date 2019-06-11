using System;
using Finances.Domain.Entity;
using Finances.Domain.Repository;

namespace Finances.Service
{
    public class OrcamentoService : IFinancesApplicationService
    {
        private IOrcamentoRepository OrcamentoRepository;
        public OrcamentoService(IOrcamentoRepository orcamentoRepository)
        {
            OrcamentoRepository = orcamentoRepository;
        }

        public Orcamento GetOrcamentoPorID(ulong id)
        {
            return OrcamentoRepository.GetByID(id);
        }

        public Orcamento GetOrcamentoPorVigencia(Vigencia vigencia)
        {
            return OrcamentoRepository.GetOrcamentoPorVigencia(vigencia);
        }

        public ulong SalvarOrcamento(Orcamento orcamento)
        {
            return OrcamentoRepository.Save(orcamento);
        }

        public OrcamentoCategoria GetOrcamentoCategoriaPorID(ulong orcamentoCategoriaID)
        {
            return OrcamentoRepository.GetOrcamentoCategoriaByID(orcamentoCategoriaID);
        }

        public void ApagarOrcamentoCategoria(ulong orcamentoCategoriaID)
        {
            OrcamentoRepository.DeleteOrcamentoCategoria(orcamentoCategoriaID);
        }

        public ulong SalvarOrcamentoCategoria(OrcamentoCategoria orcamentoCategoria)
        {
            return OrcamentoRepository.SaveOrcamentoCategoria(orcamentoCategoria);
        }
    }
}