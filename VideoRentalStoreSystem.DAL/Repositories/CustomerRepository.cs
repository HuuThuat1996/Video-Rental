using System;
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

        public IQueryable<Customer> Find(string value)
        {
            return _context.Customers.Where(x => x.CustomerID.ToString().Contains(value));
        }

        public object SelectCustomerCurrentlyOverdueDisk()
        {
            return (from cus in _context.Customers
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
    }
}
