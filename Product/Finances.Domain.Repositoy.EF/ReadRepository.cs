using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Finances.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Finances.Domain.Repository
{
    public abstract class ReadRepository<TEntity, TDbContext> : IReadRepository<TEntity>
        where TEntity: DomainEntity
        where TDbContext: DbContext
    {
        protected TDbContext Context { get; private set; }
        
        public ReadRepository(TDbContext dbContext)
        {
            Context = dbContext;
        }

        protected abstract DbSet<TEntity> DbSet { get; }       
  
        public TEntity GetByID(int id)
        {
            return GetOne(i => i.ID == id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return GetList().ToArray();
        }

        protected IQueryable<TEntity> GetList()
        {
            return this.DbSet;
        }

        protected TEntity GetOne(Expression<Func<TEntity, bool>> where)
        {
            return GetList(where).FirstOrDefault();
        }

        protected IQueryable<TEntity> GetList(Expression<Func<TEntity, bool>> where)
        {
            return GetList().Where(where);
        }        
    }
}