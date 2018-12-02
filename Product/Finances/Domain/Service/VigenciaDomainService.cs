using System;
using Finances.Domain.Entity;
using Finances.Domain.Exception;
using Finances.Domain.Repository;

namespace Finances.Domain.Service
{
    public class VigenciaDomainService
    {
       private IVigenciaRepository VigenciaRepository;
       private IOrcamentoRepository OrcamentoRepository;

        public VigenciaDomainService(IVigenciaRepository vigenciaRepository, IOrcamentoRepository orcamentoRepository)
        {
            VigenciaRepository = vigenciaRepository;
            OrcamentoRepository = orcamentoRepository;
        }
        
        public virtual Vigencia GetVigenciaAtualPorUsuario(Usuario usuario)
        {
            int referencia = GenereteReferencia(DateTime.Today.Year, DateTime.Today.Month);
            Vigencia vigenciaAtual = VigenciaRepository.GetVigencia(usuario, referencia);

            if (VigenciaNaoLocalizada(vigenciaAtual))
            {
                vigenciaAtual = CriarNovaVigencia(usuario, referencia);
                CriarOrcamento(vigenciaAtual);
            }

            return vigenciaAtual;
        }

        private bool VigenciaNaoLocalizada(Vigencia vigencia)
        {
            return vigencia is null;
        }

        private Vigencia CriarNovaVigencia(Usuario usuario, int referencia)
        {
            Vigencia vigencia = new Vigencia();
            vigencia.Referencia = referencia;
            vigencia.Usuario = usuario;

            VigenciaRepository.Save(vigencia);

            return vigencia;
        }

        private void CriarOrcamento(Vigencia vigenciaAtual)
        {
            Vigencia vigenciaAnterior = VigenciaRepository.GetVigenciaAnterior(vigenciaAtual);
            if (VigenciaNaoLocalizada(vigenciaAnterior))
                CriarOrcamentoNovo(vigenciaAtual);
            else
                CopiarOrcamentoAnterior(vigenciaAnterior, vigenciaAtual);
        }

        private void CopiarOrcamentoAnterior(Vigencia vigenciaAnterior, Vigencia vigenciaAtual)
        {
            Orcamento orcamentoAnterior = OrcamentoRepository.GetOrcamentoPorVigencia(vigenciaAnterior);
            if (orcamentoAnterior is null)
                CriarOrcamentoNovo(vigenciaAtual);
            else
            {
                Orcamento orcamento = new Orcamento();
                orcamento.Vigencia = vigenciaAtual;
                orcamento.Valor = orcamentoAnterior.Valor;

                foreach (var orcamentoCategoria in orcamentoAnterior.GetOrcamentosCategoria())
                    orcamento.AddOrcamentoCategoria(new OrcamentoCategoria() {
                        Categoria = orcamentoCategoria.Categoria,
                        Valor = orcamentoCategoria.Valor
                    });

                OrcamentoRepository.Save(orcamento);
            }
        }

        private void CriarOrcamentoNovo(Vigencia vigenciaAtual)
        {
            Orcamento orcamento = new Orcamento();
            orcamento.Vigencia = vigenciaAtual;

            OrcamentoRepository.Save(orcamento);
        }

        private int GenereteReferencia(int ano, int mes)
        {
            return ano * 100 + mes;
        }
    }
}