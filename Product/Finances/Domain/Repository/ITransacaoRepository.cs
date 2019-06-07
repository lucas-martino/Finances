using System.Collections.Generic;
using Finances.Domain.Entity;

namespace Finances.Domain.Repository
{
    public interface ITransacaoRepository : IFinancesReadRepository<Transacao>
    {
         IEnumerable<Transacao> GetTransacoesPorVigenciaPorUsuario(Vigencia vigencia, Usuario usuario);
         IEnumerable<Transacao> GetTransacoesDebitoPorVigenciaPorUsuario(Vigencia vigencia, Usuario usuario);
    }
}