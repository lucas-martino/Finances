using System;
using Finances.Domain.Entity;
using Finances.Domain.Repository;

namespace Finances.Service
{
    public class OrcamentoService : IFinancesApplicationService
    {
        private IOrcamentoRepository OrcamentoRepository;
        private VigenciaService VigenciaService;
        public OrcamentoService(IOrcamentoRepository orcamentoRepository, VigenciaService vigenciaService)
        {
            OrcamentoRepository = orcamentoRepository;
            VigenciaService = vigenciaService;
        }

        public Orcamento GetOrcamentoPorID(int id)
        {
            return OrcamentoRepository.GetByID(id);
        }

        public Orcamento GetOrcamentoPorVigencia(Vigencia vigencia)
        {
            return OrcamentoRepository.GetOrcamentoPorVigencia(vigencia);
        }

        public Orcamento GetOrcamentoVigenciaAtual(int usuarioID)
        {
            Vigencia vigenciaAtual = VigenciaService.GetVigenciaAtualPorUsuario(usuarioID);
            return GetOrcamentoPorVigencia(vigenciaAtual);
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