using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finances.Domain.Entity;
using Finances.Domain.Repository;
using Framework.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Finances.Domain.Repository.EF
{
    public class TransacaoRepository : CRUDRepository<Transacao, FinancesContext>, ITransacaoRepository
    {
        public TransacaoRepository(FinancesContext dbContext) 
            : base(dbContext)
        {
        }

        protected override DbSet<Transacao> DbSet { get { return Context.Transacoes; } }

        public IEnumerable<Transacao> GetTransacoesDebitoPorVigenciaPorUsuario(Vigencia vigencia, Usuario usuario)
        {
            return GetListPorVigenciaPorUsuario(vigencia, usuario)
                .Where(t => t.Tipo == TipoTransacao.Saida);
        }

        public IEnumerable<Transacao> GetTransacoesPorVigenciaPorUsuario(Vigencia vigencia, Usuario usuario)
        {
            return GetListPorVigenciaPorUsuario(vigencia, usuario);
        }

        private IQueryable<Transacao> GetListPorVigenciaPorUsuario(Vigencia vigencia, Usuario usuario)
        {
            return GetList(t => t.Usuario.ID == usuario.ID 
                && t.Data >= vigencia.Inicio && t.Data <= vigencia.Termino)
                .Include(t => t.Categoria).Include(c => c.Categoria.Pai);
        }
    }
}