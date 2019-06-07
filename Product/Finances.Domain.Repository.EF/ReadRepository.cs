using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Framework.Domain.Entity;
using Framework.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Finances.Domain.Repository
{
    public abstract class ReadRepository<TEntity, TDbContext> : IReadRepository<TEntity, int>
        where TEntity: DomainEntity<int>
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

        public async Task<TEntity> GetByIDAsync(int id)
        {
            return await GetOneAsync(i => i.ID == id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return GetList().ToArray();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await GetList().ToArrayAsync();
        }

        protected IQueryable<TEntity> GetList()
        {
            return this.DbSet;
        }

        protected TEntity GetOne(Expression<Func<TEntity, bool>> where)
        {
            return GetList(where).FirstOrDefault();
        }

        protected async Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> where)
        {
            return await GetList(where).FirstOrDefaultAsync();
        }

        protected IQueryable<TEntity> GetList(Expression<Func<TEntity, bool>> where)
        {
            return GetList().Where(where);
        }        
    }
}