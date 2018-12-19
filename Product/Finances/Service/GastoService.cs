using System;
using System.Collections.Generic;
using Finances.Domain.Entity;
using Finances.Domain.Repository;

namespace Finances.Service
{
    public class GastoService : IFinancesApplicationService
    {
        private IGastoRepository GastoRepository; 
        
        public GastoService(IGastoRepository gastoRepository)
        {
            GastoRepository = gastoRepository;
        }

        public IEnumerable<Gasto> GetGastosPorCategoriaEVigencia(int categoriaID, Vigencia vigencia)
        {
            return GastoRepository.GetGastosPorCategoriaEVigencia(categoriaID, vigencia);
        }

        public IEnumerable<Gasto> GetGastosNaoCategorizadoPorVigencia(Vigencia vigencia)
        {
            return GastoRepository.GetGastosNaoCategorizadoPorVigencia(vigencia);
        }

        public IEnumerable<Gasto> GetGastosPorVigencia(Vigencia vigencia)
        {
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
    }
}