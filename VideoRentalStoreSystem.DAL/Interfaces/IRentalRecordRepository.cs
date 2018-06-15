using System.Linq;

namespace VideoRentalStoreSystem.DAL.Interfaces
{
    public interface IRentalRecordRepository<T> where T: class
    {
        IQueryable<T> GetByCustomer(int customerId);
    }
}
