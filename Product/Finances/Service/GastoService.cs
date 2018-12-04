using System;
using System.Collections.Generic;
using Finances.Domain.Entity;
using Finances.Domain.Repository;

namespace Finances.Service
{
    public class GastoService : IFinancesApplicationService
    {
        private IGastoRepository GastoRepository;
        private VigenciaService VigenciaService;   
        
        public GastoService(IGastoRepository gastoRepository, VigenciaService vigenciaService, CategoriaService categoriaService)
        {
            GastoRepository = gastoRepository;
            VigenciaService = vigenciaService;
        }

        public IEnumerable<Gasto> GetGastosVigenciaAtual(int usuarioID)
        {
            return GetGastosPorVigencia(GetVigenciaAtual(usuarioID));
        }

        public IEnumerable<Gasto> GetGastosPorCategoriaVigenciaAtual(int categoriaID, int usuarioID)
        {
            return GetGastosPorCategoria(categoriaID, GetVigenciaAtual(usuarioID));
        }

        public IEnumerable<Gasto> GetGastosPorVigencia(int vigenciaID)
        {
            Vigencia vigencia = VigenciaService.GetVigenciaPorID(vigenciaID);
            return GastoRepository.GetGastosPorVigencia(vigencia);
        }

        public long SalvarGasto(Gasto entidade)
        {
            return GastoRepository.Save(entidade);
        }

        public Gasto GetGastosPorID(int id)
        {
            return GastoRepository.GetByID(id);
        }

        public void ApagarGasto(int id)
        {
            GastoRepository.Delete(id);
        }

        private IEnumerable<Gasto> GetGastosPorVigencia(Vigencia vigencia)
        {
            return GastoRepository.GetGastosPorVigencia(vigencia);
        }

        private IEnumerable<Gasto> GetGastosPorCategoria(int categoriaID, Vigencia vigencia)
        {
            return GastoRepository.GetGastosPorCategoriaEVigencia(categoriaID, vigencia);
        }

        private Vigencia GetVigenciaAtual(int usuarioID)
        {
            return VigenciaService.GetVigenciaAtualPorUsuario(usuarioID);
        }
    }
}