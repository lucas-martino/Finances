using Finances.Domain.Exception;
using Framework.Domain.Entity;
using Framework.Domain.Exception;
using Framework.Domain.Repository;
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
            int id = 0;
            if (entity != null)
            {
                ValidateEntity(entity);

                if (entity.IsNewEntity())
                    id = Create(entity);
                else
                {
                    Update(entity);
                    id = entity.ID;
                }
            }

            return id;
        }

        private void ValidateEntity(TEntity entity)
        {
            var result = entity.Validate();
            if (entity != null && !result.IsValid)
                throw new EntityInvalidException(entity, result);
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


    }
}