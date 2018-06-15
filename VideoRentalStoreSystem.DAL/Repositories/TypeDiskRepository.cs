using VideoRentalStoreSystem.DAL.DBContextEF;
using VideoRentalStoreSystem.DAL.Interfaces;

namespace VideoRentalStoreSystem.DAL.Repositories
{
    public class TypeDiskRepository : GenericRepository<DBVRContext, TypeDisk>, ITypeDiskRepository<TypeDisk>
    {
        public TypeDiskRepository(DBVRContext context) : base(context)
        {
        }
    }
}
