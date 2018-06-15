using VideoRentalStoreSystem.DAL;
using VideoRentalStoreSystem.DAL.DBContextEF;
using VideoRentalStoreSystem.DAL.Repositories;

namespace VideoRentalStoreSystem.BLL
{
    public class ManageDisk
    {
        private DiskRepository diskResponsitory;

        public ManageDisk()
        {
            diskResponsitory = new DiskRepository(new DBVRContext());
        }

        public void AddDisk(int number, string title)
        {
            for (int i = 0; i < number; i++)
            {
                Disk disk = new Disk();
                disk.Title = title;
                disk.Status = StatusOfDisk.ON_SHELF;
                diskResponsitory.Insert(disk);
            }
        }
    }
}
