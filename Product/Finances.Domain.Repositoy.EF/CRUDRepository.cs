using System.Collections.Generic;
using Finances.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Finances.Domain.Repository
{
    public abstract class CRUDRepository<TEntity, TDbContext> : ReadRepository<TEntity, TDbContext>, ICRUDRepository<TEntity, int>
        where TEntity: DomainEntity<int>
        where TDbContext: DbContext
    {
        public CRUDRepository(TDbContext dbContext)
            : base(dbContext)
        {
        }        

        public virtual int Save(TEntity entity)
        {
            if (IsNewEntity(entity))
                return Create(entity);
            else
            {
                Update(entity);
                return entity.ID;
            }
        }

        public virtual void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
            Commit();       
        }

        public virtual void Delete(int ID)
        {
            Delete(GetByID(ID));
        }

        private void Commit()
        {
            Context.SaveChanges();
        }

        private int Create(TEntity entity)
        {            
            DbSet.Add(entity);
            Commit();  

            return entity.ID;
        }

        private void Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Commit();  
        }

        private static bool IsNewEntity(TEntity entity)
        {
            return entity.ID <= 0;
        }
    }
}