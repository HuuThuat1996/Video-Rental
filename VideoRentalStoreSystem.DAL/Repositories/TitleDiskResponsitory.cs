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
    }
}
