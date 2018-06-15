using System.Collections.Generic;
using System.Linq;

namespace VideoRentalStoreSystem.DAL.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity: class
    {
        void Insert(TEntity entity);
        void Delete(TEntity entity);
        IQueryable<TEntity> GetAll();
    }
}
