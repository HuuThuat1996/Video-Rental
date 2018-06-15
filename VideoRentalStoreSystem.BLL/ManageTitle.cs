using VideoRentalStoreSystem.DAL.DBContextEF;
using VideoRentalStoreSystem.DAL.Repositories;

namespace VideoRentalStoreSystem.BLL
{
    public class ManageTitle
    {
        private DBVRContext context;
        private TitleDiskResponsitory titleResponsitory;

        public ManageTitle()
        {
            context = new DBVRContext();
            titleResponsitory = new TitleDiskResponsitory(context);
        }

        public void AddTitle(TitleDisk title)
        {
            titleResponsitory.Insert(title);
        }
    }
}
