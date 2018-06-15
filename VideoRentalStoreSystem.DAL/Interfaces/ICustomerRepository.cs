using System.Linq;

namespace VideoRentalStoreSystem.DAL.Interfaces
{
    public interface ICustomerRepository<T> where T:class
    {
        IQueryable<T> Find(string value);
    }
}
