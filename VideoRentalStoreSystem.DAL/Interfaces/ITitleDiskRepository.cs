using System.Linq;

namespace VideoRentalStoreSystem.DAL.Interfaces
{
    public interface ITitleDiskRepository<T> where T: class
    {
        IQueryable<T> Find(string value);
        void Delete(string title);
        bool IsDelete(string title);
    }
}
