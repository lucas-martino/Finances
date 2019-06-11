using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Framework.Domain.Entity;
using Framework.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Framework.Domain.Repository.EF
{
    public abstract class AbstractReadRepository<TEntity, TDbContext> : IReadRepository<TEntity>
        where TEntity: AbastractEntity
        where TDbContext: DbContext
    {
        protected TDbContext Context { get; private set; }

        public AbstractReadRepository(TDbContext dbContext)
        {
            Context = dbContext;
        }

        protected abstract DbSet<TEntity> DbSet { get; }

        public TEntity GetByID(ulong id)
        {
            return GetOne(i => i.Id == id);
        }

        public async Task<TEntity> GetByIDAsync(ulong id)
        {
            return await GetOneAsync(i => i.Id == id);
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