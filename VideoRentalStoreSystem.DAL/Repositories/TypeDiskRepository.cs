using System.Linq;
using VideoRentalStoreSystem.DAL.DBContextEF;
using VideoRentalStoreSystem.DAL.Interfaces;

namespace VideoRentalStoreSystem.DAL.Repositories
{
    public class TypeDiskRepository : GenericRepository<DBVRContext, TypeDisk>, ITypeDiskRepository<TypeDisk>
    {
        public TypeDiskRepository(DBVRContext context) : base(context)
        {
        }
        public bool Update(TypeDisk typeDisk)
        {
            if (typeDisk != null)
            {
                TypeDisk update =
                       _context.TypeDisks.Where(x => x.TypeName == typeDisk.TypeName).FirstOrDefault();
                update.Cost = typeDisk.Cost;
                update.Period = typeDisk.Period;
                update.LateCharge = typeDisk.LateCharge;
                _context.SaveChanges();
                return true;
            }

            return false;
        }
      

    }
}
