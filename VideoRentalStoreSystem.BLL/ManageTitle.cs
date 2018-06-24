﻿using VideoRentalStoreSystem.DAL.DBContextEF;
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
        /// <summary>
        /// Kiểm tra xóa tựa đề
        /// </summary>
        /// <param name="title"></param>
        /// <returns>true : tựa đề không có đĩa quá hạn hoặc đang cho thuê</returns>
        /// <returns>false : tựa đề có đĩa quá hạn hoặc đang cho thuê</returns>
        /// 
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
