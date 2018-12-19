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

        public Orcamento GetOrcamentoPorID(int id)
        {
            return OrcamentoRepository.GetByID(id);
        }

        public Orcamento GetOrcamentoPorVigencia(Vigencia vigencia)
        {
            return OrcamentoRepository.GetOrcamentoPorVigencia(vigencia);
        }

        public long SalvarOrcamento(Orcamento orcamento)
        {
            return OrcamentoRepository.Save(orcamento);
        }

        public OrcamentoCategoria GetOrcamentoCategoriaPorID(int orcamentoCategoriaID)
        {
            return OrcamentoRepository.GetOrcamentoCategoriaByID(orcamentoCategoriaID);
        }

        public void ApagarOrcamentoCategoria(int orcamentoCategoriaID)
        {
            OrcamentoRepository.DeleteOrcamentoCategoria(orcamentoCategoriaID);
        }

        public int SalvarOrcamentoCategoria(OrcamentoCategoria orcamentoCategoria)
        {
            return OrcamentoRepository.SaveOrcamentoCategoria(orcamentoCategoria);
        }
    }
}