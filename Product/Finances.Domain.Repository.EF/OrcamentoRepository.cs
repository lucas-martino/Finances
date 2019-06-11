using System;
using System.Collections.Generic;
using System.Linq;
using Finances.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Finances.Domain.Repository.EF
{
    public class OrcamentoRepository : FinancesCRUDRepository<Orcamento>, IOrcamentoRepository
    {
        private OrcamentoCategoriaRepository _orcamentoCategoriaRepository;
        public OrcamentoRepository(FinancesContext dbContext)
            : base(dbContext)
        {
            _orcamentoCategoriaRepository = new OrcamentoCategoriaRepository(dbContext);
        }

        protected override DbSet<Orcamento> DbSet { get { return Context.Orcamentos; } }

        public virtual Orcamento GetOrcamentoPorVigencia(Vigencia vigencia)
        {
            Orcamento orcamento = GetList(o => o.Vigencia.Id == vigencia.Id).FirstOrDefault();
            if (orcamento != null)
                orcamento.OrcamentosCategoria = _orcamentoCategoriaRepository.GetOrcamentoCategoriaPorOrcamento(orcamento);

            return orcamento;
        }

        public virtual OrcamentoCategoria GetOrcamentoCategoriaByID(ulong orcamentoCategoriaID)
        {
            return _orcamentoCategoriaRepository.GetByID(orcamentoCategoriaID);
        }

        public virtual void DeleteOrcamentoCategoria(ulong orcamentoCategoriaID)
        {
            _orcamentoCategoriaRepository.Delete(orcamentoCategoriaID);
        }

        public ulong SaveOrcamentoCategoria(OrcamentoCategoria orcamentoCategoria)
        {
            return _orcamentoCategoriaRepository.Save(orcamentoCategoria);
        }

        public void DeleteOrcamentoCategoriaPorCategoria(ulong categoriaID)
        {
            _orcamentoCategoriaRepository.DeleteOrcamentoCategoriaPorCategoria(categoriaID);
        }

        private class OrcamentoCategoriaRepository : FinancesCRUDRepository<OrcamentoCategoria>
        {
            public OrcamentoCategoriaRepository(FinancesContext dbContext)
                : base(dbContext)
            {
            }

            protected override DbSet<OrcamentoCategoria> DbSet { get { return Context.OrcamentosCategoria; } }

            public IList<OrcamentoCategoria> GetOrcamentoCategoriaPorOrcamento(Orcamento orcamento)
            {
                return GetList().Where(i => i.Orcamento.Id == orcamento.Id)
                    .Include(i => i.Categoria).Include(i => i.Categoria.Pai)
                    .OrderBy(c => c.Categoria.Pai != null ? string.Format("{0}{1}", c.Categoria.Pai.Nome, c.Categoria.Nome) : c.Categoria.Nome)
                    .ToList();
            }

            public void DeleteOrcamentoCategoriaPorCategoria(ulong categoriaID)
            {
                throw new NotImplementedException();
            }
        }
    }
}