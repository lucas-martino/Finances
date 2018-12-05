using Finances.Domain.Entity;

namespace Finances.Domain.Repository
{
    public interface IOrcamentoRepository : IFinancesCRUDRepository<Orcamento>
    {
        Orcamento GetOrcamentoPorVigencia(Vigencia vigencia);
        OrcamentoCategoria GetOrcamentoCategoriaByID(int orcamentoCategoriaID);
        void DeleteOrcamentoCategoria(int orcamentoCategoriaID);
        int SaveOrcamentoCategoria(OrcamentoCategoria orcamentoCategoria);
        void DeleteOrcamentoCategoriaPorCategoria(int categoriaID);
    }
}