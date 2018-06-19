using System;
using System.Data.Entity;
using System.Linq;
using VideoRentalStoreSystem.DAL.Interfaces;

namespace VideoRentalStoreSystem.DAL.Repositories
{
    public abstract class GenericRepository<TContext, TEntity> : IGenericRepository<TEntity> where TEntity : class where TContext : DbContext
    {
        public TContext _context { get; set; }
        protected IDbSet<TEntity> entities;

        protected GenericRepository(TContext context)
        {
            if (context == null)
                throw new ArgumentNullException("dbContext");
            _context = context;
            entities = context.Set<TEntity>();
        }
        public void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            _context.SaveChanges();
        }

        public void Insert(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            try
            {
                entities.Add(entity);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<TEntity> GetAll()
        {
            return entities = _context.Set<TEntity>();
        }
    }
}
