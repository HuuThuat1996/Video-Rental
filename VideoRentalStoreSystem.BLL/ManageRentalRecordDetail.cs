using System;
using System.Collections.Generic;
using VideoRentalStoreSystem.DAL.DBContextEF;
using VideoRentalStoreSystem.DAL.Repositories;

namespace VideoRentalStoreSystem.BLL
{
    public class ManageRentalRecordDetail
    {
        DBVRContext context;
        RentalRecordDetailRepository rentalRecordDetailRepository;

        public ManageRentalRecordDetail()
        {
            context = new DBVRContext();
            rentalRecordDetailRepository = new RentalRecordDetailRepository(context);
        }

        /// <summary>
        /// Initialize RentalRecordDetail with each disk 
        /// Calculate the date return, get the price information of each disk.
        /// </summary>
        /// <param name="disks"></param>
        /// <returns>List<RentalRecordDetail></returns>
        public List<RentalRecordDetail> Initialize(List<Disk> disks)
        {
            if (disks != null)
            {
                List<RentalRecordDetail> lstRentalRecordDetail = new List<RentalRecordDetail>();
                foreach (Disk disk in disks)
                {
                    RentalRecordDetail rentalRecordDetail = new RentalRecordDetail();
                    rentalRecordDetail.DiskID = disk.DiskID;
                    rentalRecordDetail.DateReturn = DateTime.Now.AddDays(disk.TitleDisk.TypeDisk.Period);
                    rentalRecordDetail.PriceRental = disk.TitleDisk.TypeDisk.Cost;
                    lstRentalRecordDetail.Add(rentalRecordDetail);
                }
                return lstRentalRecordDetail;
            }
            return null;
        }

        /// <summary>
        /// Modify list RentalRecordDetail exist
        /// </summary>
        /// <param name="disks"></param>
        /// <param name="list"></param>
        /// <returns>List<RentalRecordDetail></returns>
        public List<RentalRecordDetail> Modify(List<Disk> disks, List<RentalRecordDetail> list)
        {
            if (disks != null && list != null)
            {
                foreach (Disk disk in disks)
                {
                    if (!ContainsDisk(list, disk))
                    {
                        RentalRecordDetail rentalRecordDetail = new RentalRecordDetail();
                        rentalRecordDetail.DiskID = disk.DiskID;
                        rentalRecordDetail.DateReturn = DateTime.Now.AddDays(disk.TitleDisk.TypeDisk.Period);
                        rentalRecordDetail.PriceRental = disk.TitleDisk.TypeDisk.Cost;
                        list.Add(rentalRecordDetail);
                    }
                }
            }
            return list;
        }

        private bool ContainsDisk(List<RentalRecordDetail> list, Disk disk)
        {
            foreach (RentalRecordDetail item in list)
            {
                if (item.DiskID == disk.DiskID)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
