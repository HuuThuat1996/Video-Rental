using System;
using System.Transactions;
using VideoRentalStoreSystem.DAL;
using VideoRentalStoreSystem.DAL.DBContextEF;
using VideoRentalStoreSystem.DAL.Repositories;

namespace VideoRentalStoreSystem.BLL
{
    /// <summary>
    /// Add a late charge if a customer returns a DVD or disk late.
    /// The charge is computed and added automatically when the DVD or disk is returned.
    /// </summary>
    public class AddLateCharge
    {
        RentalRecordDetailRepository detailRepository;
        public AddLateCharge()
        {
            detailRepository = new RentalRecordDetailRepository(new DBVRContext());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="diskId"></param>
        /// <returns>
        /// Have late charge and add success return 1
        /// Have late charge and add fail return -1
        /// Not have late charge return 0
        /// Order return -2
        /// </returns>
        public int Add(int diskId, DateTime DateReturn)
        {
            //Check status disk
            DiskRepository diskRepository = new DiskRepository(new DBVRContext());
            Disk disk = diskRepository.Get(diskId);
            if (disk != null)
            {
                if (disk.Status.Equals(StatusOfDisk.RENTED))
                {
                    RentalRecordDetail detail = detailRepository.GetLatest(diskId);
                    if (detail != null)
                    {
                        using (TransactionScope transaction = new TransactionScope())
                        {
                            try
                            {
                                    detail.DateReturnActual = DateReturn;
                                    detail.LateCharge = disk.TitleDisk.TypeDisk.LateCharge;
                                    detailRepository.Update(detail);
                                    diskRepository.ModifyStatus(disk, StatusOfDisk.ON_SHELF);
                                    transaction.Complete();
                                    return 1;
                            }
                            catch
                            {
                                transaction.Dispose();
                                return 0;
                            }
                        }
                    }
                    return -1;
                }
            }
            return -2;
        }
    }
}
