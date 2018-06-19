using System.Linq;

namespace VideoRentalStoreSystem.DAL.Interfaces
{
    public interface ICustomerRepository<T> where T:class
    {
        IQueryable<T> FindCustomerID(string value);
        IQueryable<T> FindCustomerName(string value);
    }
}
