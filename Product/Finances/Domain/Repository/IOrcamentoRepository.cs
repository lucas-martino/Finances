using Finances.Domain.Entity;

namespace Finances.Domain.Repository
{
    public interface IOrcamentoRepository : IFinancesCRUDRepository<Orcamento>
    {
        Orcamento GetOrcamentoPorVigencia(Vigencia vigencia);
        OrcamentoCategoria GetOrcamentoCategoriaByID(ulong orcamentoCategoriaID);
        void DeleteOrcamentoCategoria(ulong orcamentoCategoriaID);
        ulong SaveOrcamentoCategoria(OrcamentoCategoria orcamentoCategoria);
        void DeleteOrcamentoCategoriaPorCategoria(ulong categoriaID);
    }
}