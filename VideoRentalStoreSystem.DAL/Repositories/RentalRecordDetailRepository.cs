using System.Linq;
using VideoRentalStoreSystem.DAL.DBContextEF;
using VideoRentalStoreSystem.DAL.Interfaces;

namespace VideoRentalStoreSystem.DAL.Repositories
{
    public class RentalRecordDetailRepository : GenericRepository<DBVRContext, RentalRecordDetail>, IRentalRecordDetailRepository<RentalRecordDetail>
    {
        public RentalRecordDetailRepository(DBVRContext context) : base(context)
        {
        }

        public RentalRecordDetail GetLatest(int diskId)
        {
            return _context.RentalRecordDetails.OrderByDescending(y => y.DateReturn).Where(x => x.DiskID == diskId && x.DateReturnActual == null).FirstOrDefault();
        }

        public void Update(RentalRecordDetail detail)
        {
            if (detail != null)
            {
                _context.RentalRecordDetails.Attach(detail);
                var entry = _context.Entry(detail);
                entry.Property(e => e.DateReturnActual).IsModified = true;
                entry.Property(e => e.LateCharge).IsModified = true;
                _context.SaveChanges();
            }
        }
    }
}
