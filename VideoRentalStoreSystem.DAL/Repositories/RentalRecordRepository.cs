using System.Linq;
using VideoRentalStoreSystem.DAL.DBContextEF;
using VideoRentalStoreSystem.DAL.Interfaces;

namespace VideoRentalStoreSystem.DAL.Repositories
{
    public class RentalRecordRepository : GenericRepository<DBVRContext, RentalRecord>, IRentalRecordRepository<RentalRecord>
    {
        public RentalRecordRepository(DBVRContext context) : base(context)
        {
        }

        public IQueryable<RentalRecord> GetByCustomer(int customerId)
        {
            return _context.RentalRecords.Where(x => x.CustomerID == customerId);
        }
        public RentalRecord Get(int ID)
        {
            return _context.RentalRecords.Where(x => x.RentalRecordID == ID).FirstOrDefault();

        }
    }
}
