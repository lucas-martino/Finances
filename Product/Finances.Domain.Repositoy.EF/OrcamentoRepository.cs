using System;
using System.Collections.Generic;
using System.Linq;
using Finances.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Finances.Domain.Repository
{
    public class OrcamentoRepository : CRUDRepository<Orcamento, FinancesContext>, IOrcamentoRepository
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
            Orcamento orcamento = GetList(o => o.Vigencia.ID == vigencia.ID).FirstOrDefault();
            if (orcamento != null)
                orcamento.OrcamentosCategoria = _orcamentoCategoriaRepository.GetOrcamentoCategoriaPorOrcamento(orcamento);
                
            return orcamento;
        }

        public virtual OrcamentoCategoria GetOrcamentoCategoriaByID(int orcamentoCategoriaID)
        {
            return _orcamentoCategoriaRepository.GetByID(orcamentoCategoriaID);
        }

        public virtual void DeleteOrcamentoCategoria(int orcamentoCategoriaID)
        {
            _orcamentoCategoriaRepository.Delete(orcamentoCategoriaID);
        }

        public int SaveOrcamentoCategoria(OrcamentoCategoria orcamentoCategoria)
        {
            return _orcamentoCategoriaRepository.Save(orcamentoCategoria);
        }

        public void DeleteOrcamentoCategoriaPorCategoria(int categoriaID)
        {
            _orcamentoCategoriaRepository.DeleteOrcamentoCategoriaPorCategoria(categoriaID);
        }

        private class OrcamentoCategoriaRepository : CRUDRepository<OrcamentoCategoria, FinancesContext>
        {
            public OrcamentoCategoriaRepository(FinancesContext dbContext) 
                : base(dbContext)
            {
            }

            protected override DbSet<OrcamentoCategoria> DbSet { get { return Context.OrcamentosCategoria; } }

            public IList<OrcamentoCategoria> GetOrcamentoCategoriaPorOrcamento(Orcamento orcamento)
            {
                return GetList().Where(i => i.Orcamento.ID == orcamento.ID).Include(i => i.Categoria).ToList();
            }

            public void DeleteOrcamentoCategoriaPorCategoria(int categoriaID)
            {
                throw new NotImplementedException();
            }
        }
    }
}