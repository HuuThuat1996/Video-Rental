using System.Collections.Generic;
using System.Linq;
using VideoRentalStoreSystem.DAL.DBContextEF;
using VideoRentalStoreSystem.DAL.Interfaces;

namespace VideoRentalStoreSystem.DAL.Repositories
{
    public class TitleDiskResponsitory : GenericRepository<DBVRContext, TitleDisk>, ITitleDiskRepository<TitleDisk>
    {
        public TitleDiskResponsitory(DBVRContext context) : base(context)
        {
        }

        public IQueryable<TitleDisk> Find(string value)
        {
            return _context.TitleDisks.Where(x => x.Title.ToLower().Contains(value.ToLower()));
        }
        public void Delete(string title)
        {

            _context.TitleDisks.Remove(_context.TitleDisks.ToList().Where(x => x.Title == title).FirstOrDefault());
            _context.SaveChanges();

        }
        public bool IsDelete(string title)
        {
            TitleDisk titleDisk = _context.TitleDisks.ToList().Where(x => x.Title == title).FirstOrDefault();
            if (titleDisk.Disks.Where(x => x.Status == StatusOfDisk.RENTED.ToString()).ToList().Count() == 0
                || titleDisk.Disks.Count == 0)
                return true;
            return false;
        }


    }
}
