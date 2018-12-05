using System;
using Finances.Domain.Entity;
using Finances.Domain.Exception;
using Finances.Domain.Repository;

namespace Finances.Domain.Service
{
    public class CategoriaDomainService
    {
        private ICategoriaRepository CategoriaRepository;
        private IGastoRepository GastoRepository;
        private IOrcamentoRepository OrcamentoRepository;
        public CategoriaDomainService(ICategoriaRepository categoriaRepository, IGastoRepository gastoRepository, IOrcamentoRepository orcamentoRepository)
        {
            CategoriaRepository = categoriaRepository;
            GastoRepository = gastoRepository;
            OrcamentoRepository = orcamentoRepository;
        }

        public virtual int SaveCategoria(Categoria categoria)
        {
            if (IsCreate(categoria) && PossuiCategoriaMesmoNome(categoria))
                throw new NomeCategoriaDuplicadoException(categoria.Nome);

            return CategoriaRepository.Save(categoria);
        }

        public virtual void DeleteCategoria(int categoriaID)
        {
            if (PossuiGastoVinculado(categoriaID))
                throw new CategoriaComLancemntosException();
                
            //DeleteDepenciasCategoria(categoriaID);
            CategoriaRepository.Delete(categoriaID);
        }

        private static bool IsCreate(Categoria categoria)
        {
            return categoria != null && categoria.IsNewEntity();
        }

        private void DeleteDepenciasCategoria(int categoriaID)
        {
            OrcamentoRepository.DeleteOrcamentoCategoriaPorCategoria(categoriaID);
        }

        private bool PossuiCategoriaMesmoNome(Categoria categoria)
        {
            return CategoriaRepository.GetCategoriaPorNomeUsuario(categoria.Nome, categoria.Usuario) != null;
        }

        private bool PossuiGastoVinculado(int categoriaID)
        {
            return GastoRepository.GetGastoTotalPorCategoria(categoriaID) > 0;
        }
    }
}