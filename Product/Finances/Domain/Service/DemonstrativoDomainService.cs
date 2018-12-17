using System;
using System.Linq;
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
        private ICategoriaRepository CategoriaRepository { get; set; }
        
        public DemonstrativoDomainService(IGastoRepository gastoRepository, IOrcamentoRepository orcamentoRepository, ICategoriaRepository categoriaRepository)
        {
            GastoRepository = gastoRepository;
            OrcamentoRepository = orcamentoRepository;
            CategoriaRepository = categoriaRepository;
        }

        public Demonstrativo GenereteDemonstrativo(Vigencia vigencia)
        {
            Demonstrativo demonstrativo = new Demonstrativo();
            GenereteDemonstrativoParcial(demonstrativo, vigencia);
            demonstrativo.Categorias = GenereteDemonstrativoCategorias(vigencia, demonstrativo.ValorGastoTotal);

            return demonstrativo;
        }

        public virtual DemonstrativoParcial GenereteDemonstrativoParcial(Vigencia vigencia)
        {
            return GenereteDemonstrativoParcial(new DemonstrativoParcial(), vigencia);
        }

        private IList<DemonstrativoItemCategoria> GenereteDemonstrativoCategorias(Vigencia vigencia, decimal valorTotal)
        {
            IList<DemonstrativoItemCategoria> lista = new List<DemonstrativoItemCategoria>();

            DemonstrativoItemCategoria item;
            foreach (var categoria in CategoriaRepository.GetCategoriaPorUsuario(vigencia.Usuario))
            {
                item = new DemonstrativoItemCategoria();
                item.Categoria = categoria;
                item.ValorGastoCompleto = GetGastoTotalCompletoPorCategoria(categoria, vigencia);
                item.ValorGasto = GetGastoTotalPorCategoria(categoria, vigencia);
                item.Percentual = CalculePercentual(valorTotal, item.ValorGastoCompleto);
                item.Cor = COR_PADRAO;

                lista.Add(item);
            }

            return lista;
        }

        private static decimal CalculePercentual(decimal valorTotal, decimal valorGasto)
        {
            return (valorTotal > 0) ? valorGasto * 100 / valorTotal : 0;
        }

        private DemonstrativoParcial GenereteDemonstrativoParcial(DemonstrativoParcial demonstrativo, Vigencia vigencia)
        {
            Orcamento orcamento = GetOrcamento(vigencia);
            demonstrativo.ValorGastoTotal = GetGastoTotal(vigencia);
            demonstrativo.OrcamentoTotal = orcamento.Valor - demonstrativo.ValorGastoTotal;
            demonstrativo.Cor = GetCor(orcamento.Valor, demonstrativo.ValorGastoTotal);
            demonstrativo.Orcamentos = GenereteDemonstrativoOrcamentoCategoria(orcamento);
            demonstrativo.NaoCategorizado = GenereteDemonstrativoNaoCategorizado(vigencia, demonstrativo.ValorGastoTotal);

            return demonstrativo;
        }

        private DemonstrativoItem GenereteDemonstrativoNaoCategorizado(Vigencia vigencia, decimal valorTotal)
        {
            DemonstrativoItem demonstrativoNaoCategorizado = null;
            decimal valorGastoNaoCategorizado = GetGastoNaoCategorizado(vigencia);
            if (valorGastoNaoCategorizado > 0 )
            {
                demonstrativoNaoCategorizado = new DemonstrativoItem();
                demonstrativoNaoCategorizado.ValorGastoCompleto = valorGastoNaoCategorizado;
                demonstrativoNaoCategorizado.Percentual = CalculePercentual(valorTotal, demonstrativoNaoCategorizado.ValorGastoCompleto);
                demonstrativoNaoCategorizado.Cor = NAO_CATEGORIZADO;
            }
            
            return demonstrativoNaoCategorizado;
        }

        private decimal GetGastoNaoCategorizado(Vigencia vigencia)
        {
            return GastoRepository.GetGastoTotalVigenciaSemCategoria(vigencia);
        }

        private IList<DemonstrativoItemOrcamento> GenereteDemonstrativoOrcamentoCategoria(Orcamento orcamento)
        {
            IList<DemonstrativoItemOrcamento> lista = new List<DemonstrativoItemOrcamento>();

           DemonstrativoItemOrcamento item;
            foreach (var orcamentoCategoria in orcamento.OrcamentosCategoria)
            {
                item = new DemonstrativoItemOrcamento();
                item.Categoria = orcamentoCategoria.Categoria;
                item.ValorGastoCompleto = GetGastoTotalCompletoPorCategoria(orcamentoCategoria.Categoria, orcamento.Vigencia);
                item.ValorGasto = GetGastoTotalPorCategoria(orcamentoCategoria.Categoria, orcamento.Vigencia);
                item.OrcamentoRestante = orcamentoCategoria.Valor - item.ValorGastoCompleto;
                item.Cor = GetCor(orcamentoCategoria.Valor, item.ValorGasto);
                
                lista.Add(item);
            }

            return lista;
        }

        private decimal GetGastoTotalPorCategoria(Categoria categoria, Vigencia vigencia)
        {
            return GastoRepository.GetGastoTotalPorCategoriaEVigencia(categoria, vigencia);
        }

        private decimal GetGastoTotalCompletoPorCategoria(Categoria categoria, Vigencia vigencia)
        {
            return GastoRepository.GetGastoTotalCompletoPorCategoriaEVigencia(categoria, vigencia);
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