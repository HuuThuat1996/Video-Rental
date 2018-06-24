using System.Collections.Generic;
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
        /// <summary>
        /// Lấy ra khách hàng có đĩa trả trễ
        /// </summary>
        /// <param name="DiskID"> mã đĩa trả trễ</param>
        /// <returns>khách hàng</returns>
        public Customer GetCustomerByDiskLateCharge(int DiskID)
        {
            // lấy danh sách hóa đơn có đĩa quá hạn.
            RentalRecordDetail rentalRecordDetail = new RentalRecordDetail();
            rentalRecordDetail = _context.RentalRecordDetails.Where(x => x.DiskID == DiskID && x.LateCharge != null).FirstOrDefault();
            RentalRecord rentalRecord = new RentalRecord();
            rentalRecord = _context.RentalRecords.Where(x => x.RentalRecordID.Equals(rentalRecordDetail.RentalRecordID)).FirstOrDefault();
            return _context.Customers.Where(c => c.CustomerID.Equals(rentalRecord.CustomerID)).FirstOrDefault();
        }
        public List<RentalRecordDetail> GetInformationLateCharges(int customerID)
        {
            return _context.RentalRecordDetails.Where(x => x.RentalRecordID == x.RentalRecord.RentalRecordID && customerID == x.RentalRecord.CustomerID && x.LateCharge != null).ToList();
        }
        public void UpdateLateCharge(RentalRecordDetail detail)
        {
            if (detail != null)
            {
                RentalRecordDetail update =
                         _context.RentalRecordDetails.Where(x => x.RentalRecordID == detail.RentalRecordID && x.DiskID == detail.DiskID).FirstOrDefault();
                update.LateCharge = null;
                _context.SaveChanges();
            }
        }
    }
}
