using System.Collections.Generic;
using System.Linq;
using VideoRentalStoreSystem.DAL.DBContextEF;
using VideoRentalStoreSystem.DAL.Interfaces;
using VideoRentalStoreSystem.DAL.Models;

namespace VideoRentalStoreSystem.DAL.Repositories
{
    public class DiskRepository : GenericRepository<DBVRContext, Disk>, IDiskRepository<Disk>
    {
        public DiskRepository(DBVRContext context) : base(context)
        {
        }

        public void ModifyStatus(Disk disk, string status)
        {
            if (disk != null && !string.IsNullOrEmpty(status))
            {
                disk.Status = status;
                var entity = _context.Disks.Find(disk.DiskID);
                if (entity != null)
                {
                    _context.Entry(entity).CurrentValues.SetValues(disk);
                    _context.SaveChanges();
                }
            }
        }

        public void ModifyStatus(List<int> listDiskId, string status)
        {
            if (listDiskId != null && !string.IsNullOrEmpty(status))
            {
                foreach (int id in listDiskId)
                {
                    var disk = _context.Disks.Find(id);
                    disk.Status = status;
                    _context.Disks.Attach(disk);
                    var entry = _context.Entry(disk);
                    entry.Property(e => e.Status).IsModified = true;
                }
                _context.SaveChanges();
            }
        }

        public List<DiskInformation> GetInformationDisk(string title)
        {
            List<DiskInformation> lstDiskInformation = new List<DiskInformation>();

            TitleDisk titleDisk = _context.TitleDisks.Find(title);
            if (titleDisk != null)
            {
                List<DiskInformation> lstDiskRented = (from disk in titleDisk.Disks
                                                       join detail in _context.RentalRecordDetails
                                                       on disk.DiskID equals detail.DiskID
                                                       join record in _context.RentalRecords
                                                       on detail.RentalRecordID equals record.RentalRecordID
                                                       orderby detail.DateReturn
                                                       where disk.Status.Equals(StatusOfDisk.RENTED)
                                                       && detail.LateCharge is null
                                                       && detail.DateReturnActual is null
                                                       select (
                                                       new DiskInformation
                                                       {
                                                           DiskID = disk.DiskID,
                                                           Status = disk.Status,
                                                           CustomerName = record.Customer.Name,
                                                           DateReturn = detail.DateReturn,
                                                           DateResevartion = null
                                                       })).ToList();

                List<DiskInformation> lstDiskOn_Hold = (from disk in titleDisk.Disks
                                                        join res in _context.Reservations
                                                        on disk.DiskID equals res.DiskID
                                                        orderby res.DateReservation
                                                        where disk.Status.Equals(StatusOfDisk.ON_HOLD)
                                                        select (
                                                        new DiskInformation
                                                        {
                                                            DiskID = disk.DiskID,
                                                            Status = disk.Status,
                                                            CustomerName = res.Customer.Name,
                                                            DateReturn = null,
                                                            DateResevartion = res.DateReservation
                                                        })).ToList();
                List<DiskInformation> lstDiskOn_Shelf = (from disk in titleDisk.Disks
                                                         where disk.Status.Equals(StatusOfDisk.ON_SHELF)
                                                         select (
                                                         new DiskInformation
                                                         {
                                                             DiskID = disk.DiskID,
                                                             Status = disk.Status,
                                                             CustomerName = null,
                                                             DateReturn = null,
                                                             DateResevartion = null
                                                         })).ToList();

                lstDiskInformation = lstDiskRented;
                lstDiskInformation.AddRange(lstDiskOn_Shelf);
                lstDiskInformation.AddRange(lstDiskOn_Hold);
            }
            return lstDiskInformation.OrderBy(x => x.DiskID).ToList();
        }

        public object GetInfoAllDisk()
        {
            string query = "select disk.Title, disk.Status as StatusDisk, COUNT(disk.DiskID) as Quantity from Disk disk group by disk.Title, disk.Status";
            var result = _context.Database.SqlQuery<InfoReportTitle>(query);
            return result;
        }

        public IQueryable<Disk> Get(string id, string status)
        {
            return _context.Disks.OrderBy(x => x.DiskID).Where(y => y.DiskID.ToString().Contains(id) && y.Status.Equals(status));
        }

        public Disk Get(int id)
        {
            return _context.Disks.Find(id);
        }

        public IQueryable<Disk> Get(string status)
        {
            return _context.Disks.OrderBy(y => y.DiskID).Where(x => x.Status.Equals(status));
        }

        public IQueryable<Disk> GetByTitle(string title)
        {
            return _context.Disks.Where(x => x.Title == title && x.Status != StatusOfDisk.DELETE);
        }

        public void Modify(Disk entity)
        {
            throw new System.NotImplementedException();
        }

        public void Modify(List<int> lst, string status)
        {
            throw new System.NotImplementedException();
        }
        public IQueryable<Disk> GetDisks()
        {
            return _context.Disks.OrderBy(y => y.DiskID).Where(x => x.Status != StatusOfDisk.DELETE);
        }
        public void DeleteDisk(List<Disk> listDisk)
        {
            foreach (Disk disk in listDisk)
            {
                Disk diskDelete =
                    _context.Disks.Where(x => x.DiskID.Equals(disk.DiskID)).FirstOrDefault();
                diskDelete.Status = StatusOfDisk.DELETE;

            }
            _context.SaveChanges();

        }
        public bool IsDelete(Disk disk)
        {
            if(_context.RentalRecordDetails.Where(x => x.DiskID.Equals(disk.DiskID) && x.LateCharge != null).ToList().Count!=0)
            {
                return false;
            }
            return true;
        }
    }
}
