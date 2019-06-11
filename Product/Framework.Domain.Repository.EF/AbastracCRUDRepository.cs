using System.Threading.Tasks;
using Framework.Domain.Entity;
using Framework.Domain.Exception;
using Framework.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Framework.Domain.Repository.EF
{
    public abstract class AbastracCRUDRepository<TEntity, TDbContext> : AbstractReadRepository<TEntity, TDbContext>, ICRUDRepository<TEntity>
        where TEntity: AbastractEntity
        where TDbContext: DbContext
    {
        public AbastracCRUDRepository(TDbContext dbContext)
            : base(dbContext)
        {
        }

        public virtual ulong Save(TEntity entity)
        {
            ulong id = 0;
            if (entity != null)
            {
                ValidateEntity(entity);

                if (entity.IsNewEntity())
                    Create(entity);
                else
                    Update(entity);

                Commit();
                id = entity.Id;
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

        public virtual void Delete(ulong ID)
        {
            Delete(GetByID(ID));
        }

        private void Commit()
        {
            Context.SaveChanges();
        }

        private void CommitAsync()
        {
            Context.SaveChangesAsync();
        }

        private void Create(TEntity entity)
        {
            DbSet.Add(entity);
        }

        private void Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }
    }
}