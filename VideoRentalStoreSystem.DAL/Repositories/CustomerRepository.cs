using System;
using System.Collections.Generic;
using System.Linq;
using VideoRentalStoreSystem.DAL.DBContextEF;
using VideoRentalStoreSystem.DAL.Interfaces;
using VideoRentalStoreSystem.DAL.Models;

namespace VideoRentalStoreSystem.DAL.Repositories
{
    public class CustomerRepository : GenericRepository<DBVRContext, Customer>, ICustomerRepository<Customer>
    {
        public CustomerRepository(DBVRContext context) : base(context)
        {
        }

        public IQueryable<Customer> FindCustomerID(string value)
        {
            return _context.Customers.Where(x => x.CustomerID.ToString().Contains(value) && x.IsDeleted == StatusCustomer.Already);
        }

        public IQueryable<Customer> FindCustomerName(string value)
        {
            return _context.Customers.Where(x => x.Name.ToString().Contains(value) && x.IsDeleted == StatusCustomer.Already);
        }
        public object SelectCustomerCurrentlyOverdueDisk()
        {
            return (from cus in _context.Customers
                    where cus.IsDeleted == StatusCustomer.Already
                    join ren in _context.RentalRecords
                    on cus.CustomerID equals ren.CustomerID
                    join de in _context.RentalRecordDetails
                    on ren.RentalRecordID equals de.RentalRecordID
                    join disk in _context.Disks
                    on de.DiskID equals disk.DiskID
                    where disk.Status.Equals(StatusOfDisk.RENTED)
                    && de.DateReturnActual == null
                    && DateTime.Now > de.DateReturn
                    select new InfoReportCustomer
                    {
                        CustomerID = cus.CustomerID,
                        Name = cus.Name,
                        Address = cus.Address,
                        PhoneNumber = cus.PhoneNumber,
                        DiskID = de.DiskID,
                        Title = de.Disk.Title,
                        DateReturn = de.DateReturn
                    });
        }
        public void UpdateCustomer(Customer customer)
        {
            Customer update =
                       _context.Customers.Where(x => x.CustomerID == customer.CustomerID).FirstOrDefault();
            update.Name = customer.Name;
            update.Address = customer.Address;
            update.PhoneNumber = customer.PhoneNumber;
            _context.SaveChanges();
        }
        public void DeleteCustomer(int customerID)
        {
            // xóa tất cả danh sách đặt đĩa của khách hàng đó
            _context.Customers.Remove(_context.Customers.Where(x => x.CustomerID == customerID).FirstOrDefault());
            // lấy danh sách đĩa đang thuê của khách hàng.
            List<Disk> updateDisk =
                     _context.Disks.Where(x => x.Status == StatusOfDisk.RENTED
                     && x.DiskID == x.RentalRecordDetails
                                    .Where(r => r.RentalRecord.CustomerID == customerID)
                                    .Select(s => s.DiskID).FirstOrDefault()).ToList();
            _context.Reservations.RemoveRange(_context.Reservations.Where(x => x.CustomerID == customerID));
            foreach (Disk disk in updateDisk)
            {
                disk.Status = StatusOfDisk.DELETE;
            }
            _context.SaveChanges();
        }
        public List<string> IsDelete(int customerID)
        {
            List<string> listLate = new List<string>();
            List<int> listRental = new List<int>();
            // lấy danh sách các hóa đơn của khách hàng
            listRental = _context.RentalRecords.Where(x => x.CustomerID == customerID)
                .Select(s => s.RentalRecordID).ToList();
            foreach (int id in listRental)
            {
                // kiểm tra khách hàng còn đĩa thuê nhưng chưa trả
                if (_context.RentalRecordDetails.Where(x => x.RentalRecordID == id)
                    .Select(s => s.DateReturnActual)
                    .Where(d => d.Value == null).Count() > 0)
                    listLate.Add(StatusCustomer.DiskLate);
                // Kiếm tra khách hàng có phí trễ hạn
                if (_context.RentalRecordDetails.Where(x => x.LateCharge != null).ToList().Count != 0)
                    listLate.Add(StatusCustomer.LateCharge);
            }

            return listLate;
        }
        public List<Customer> GetCustomers()
        {
            return _context.Customers.Where(x => x.IsDeleted == StatusCustomer.Already).ToList();
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
            return _context.Customers.Where(c => c.CustomerID.Equals(rentalRecord.RentalRecordID)).FirstOrDefault();
        }
        public List<RentalRecordDetail> GetInformationLateCharges(Customer customer)
        {
          return  _context.RentalRecordDetails.Where(x => x.RentalRecordID == x.RentalRecord.RentalRecordID && customer.CustomerID == x.RentalRecord.CustomerID).ToList();
        }
    }
}
