using VideoRentalStoreSystem.DAL.DBContextEF;
using VideoRentalStoreSystem.DAL.Repositories;

namespace VideoRentalStoreSystem.BLL
{
    public class ManageTitle
    {

        private TitleDiskResponsitory titleResponsitory;
        private TypeDiskRepository typeDiskRepository;
        public ManageTitle()
        {
            typeDiskRepository = new TypeDiskRepository(new DBVRContext());
            titleResponsitory = new TitleDiskResponsitory(new DBVRContext());
        }
        /// <summary>
        /// Thêm tựa đề
        /// </summary>
        /// <param name="title">tựa đề</param>
        public void AddTitle(TitleDisk title)
        {
            titleResponsitory.Insert(title);
        }
        public bool IsDelete(TitleDisk title)
        {
            if (titleResponsitory.IsDelete(title.Title))
                return true;
            return false;
        }
        public void Delete (string title)
        {
            titleResponsitory.Delete(title);
        }
    }
}
