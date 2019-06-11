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

        public IEnumerable<Gasto> GetGastosPorCategoriaEVigencia(ulong categoriaID, Vigencia vigencia)
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

        public ulong SalvarGasto(Gasto entidade)
        {
            return GastoRepository.Save(entidade);
        }

        public Gasto GetGastosPorID(ulong id)
        {
            return GastoRepository.GetByID(id);
        }

        public void ApagarGasto(ulong id)
        {
            GastoRepository.Delete(id);
        }
    }
}