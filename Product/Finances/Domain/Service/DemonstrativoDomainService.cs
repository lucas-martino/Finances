using System;
using System.Collections.Generic;
using Finances.Domain.Entity;
using Finances.Domain.Repository;

namespace Finances.Domain.Service
{
    public class DemonstrativoDomainService
    {
        private const string COR_PADRAO = "Black";
        private const string ORCAMENTO_POSITIVO = "Green";
        private const string ORCAMENTO_NEGATIVO = "Red";
        private const string NAO_CATEGORIZADO = "Orange";
        private IGastoRepository GastoRepository { get; set; }
        private IOrcamentoRepository OrcamentoRepository { get; set; }
        
        public DemonstrativoDomainService(IGastoRepository gastoRepository, IOrcamentoRepository orcamentoRepository)
        {
            GastoRepository = gastoRepository;
            OrcamentoRepository = orcamentoRepository;
        }

        public virtual DemonstrativoParcial GenereteDemonstrativoParcial(Vigencia vigencia)
        {
            DemonstrativoParcial demonstrativo = new DemonstrativoParcial();
            Orcamento orcamento = GetOrcamento(vigencia);
            demonstrativo.ValorGastoTotal = GetGastoTotal(vigencia);
            demonstrativo.Cor = GetCor(orcamento.Valor, demonstrativo.ValorGastoTotal);
            demonstrativo.Orcamentos = GenereteDemonstrativoOrcamentoCategoria(orcamento);
            demonstrativo.NaoCategorizado = GenereteDemonstrativoNaoCategorizado(vigencia);

            return demonstrativo;
        }

        private DemonstrativoNaoCategorizado GenereteDemonstrativoNaoCategorizado(Vigencia vigencia)
        {
            DemonstrativoNaoCategorizado demonstrativoNaoCategorizado = null;
            decimal valorGastoNaoCategorizado = GetGastoNaoCategorizado(vigencia);
            if (valorGastoNaoCategorizado > 0 )
            {
                demonstrativoNaoCategorizado = new DemonstrativoNaoCategorizado();
                demonstrativoNaoCategorizado.ValorGasto = valorGastoNaoCategorizado;
                demonstrativoNaoCategorizado.Cor = NAO_CATEGORIZADO;
            }
            
            return demonstrativoNaoCategorizado;
        }

        private decimal GetGastoNaoCategorizado(Vigencia vigencia)
        {
            return GastoRepository.GetGastoTotalVigenciaSemCategoria(vigencia);
        }

        private IList<DemonstrativoItem> GenereteDemonstrativoOrcamentoCategoria(Orcamento orcamento)
        {
            IList<DemonstrativoItem> lista = new List<DemonstrativoItem>();

           DemonstrativoItem item;
            foreach (var orcamentoCategoria in orcamento.OrcamentosCategoria)
            {
                item = new DemonstrativoItem();
                item.Categoria = orcamentoCategoria.Categoria;
                item.ValorGasto = GetGastoTotalPorCategoria(orcamentoCategoria.Categoria, orcamento.Vigencia);
                item.OrcamentoRestante = orcamentoCategoria.Valor - item.ValorGasto;
                item.Cor = GetCor(orcamentoCategoria.Valor, item.ValorGasto);
                
                lista.Add(item);
            }

            return lista;
        }

        private decimal GetGastoTotalPorCategoria(Categoria categoria, Vigencia vigencia)
        {
            return GastoRepository.GetGastoTotalPorCategoriaEVigencia(categoria, vigencia);
        }

        private static string GetCor(decimal orcamento, decimal gasto)
        {
            string cor;
            if (PossuiOrcamento(orcamento))
            {  
                if (IsOrcamentoPositivo(orcamento, gasto))
                    cor = ORCAMENTO_POSITIVO;
                else 
                    cor = ORCAMENTO_NEGATIVO;
            } 
            else
                cor = COR_PADRAO; 

            return cor;
        }

        private static bool PossuiOrcamento(decimal orcamento)
        {
            return (orcamento > 0);
        }

        private static bool IsOrcamentoPositivo(decimal orcamento, decimal gasto)
        {
            if (PossuiOrcamento(orcamento))
                return gasto <= orcamento;
            
            return true;
        }

        private Orcamento GetOrcamento(Vigencia vigencia)
        {
            return OrcamentoRepository.GetOrcamentoPorVigencia(vigencia);
        }

        private decimal GetGastoTotal(Vigencia vigencia)
        {
            return GastoRepository.GetGastoTotalPorVigencia(vigencia);
        }
    }
}