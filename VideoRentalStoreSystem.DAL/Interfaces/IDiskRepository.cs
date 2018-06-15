using System.Collections.Generic;
using System.Linq;

namespace VideoRentalStoreSystem.DAL.Interfaces
{
    public interface IDiskRepository<T> where T : class
    {
        T Get(int id);
        IQueryable<T> Get(string status);
        IQueryable<T> Get(string id, string status);
        IQueryable<T> GetByTitle(string title);
        void Modify(T entity);
        void Modify(List<int> lst, string status);
    }
}
