using System;
using System.Transactions;
using VideoRentalStoreSystem.DAL;
using VideoRentalStoreSystem.DAL.DBContextEF;
using VideoRentalStoreSystem.DAL.Repositories;

namespace VideoRentalStoreSystem.BLL
{
    public class ManageReturnDisk
    {
        private RentalRecordDetailRepository detailRepository;
        private DiskRepository diskRepository;

        public ManageReturnDisk( DBVRContext context)
        {
            detailRepository = new RentalRecordDetailRepository(context);
            diskRepository = new DiskRepository(context);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="disk"></param>
        /// <returns>
        /// Have late charge return 1
        /// Not have late charge return 0
        /// error return 1
        /// exception return 2
        /// </returns>
        public int ReturnDisk(Disk disk)
        {
            if (disk != null)
            {
                AddLateCharge addLateCharge = new AddLateCharge();
                RentalRecordDetail detailLatest = detailRepository.GetLatest(disk.DiskID);
                if (detailLatest != null)
                {
                    if (DateTime.Now.Date > detailLatest.DateReturn.Date)
                    {
                        int result = addLateCharge.Add(disk.DiskID);
                        if (result >= 0)
                        {
                            return 1;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        using (TransactionScope transaction = new TransactionScope())
                        {
                            try
                            {
                                detailLatest.DateReturnActual = DateTime.Now;
                                detailRepository.Update(detailLatest);
                                diskRepository.ModifyStatus(disk, StatusOfDisk.ON_SHELF);
                                transaction.Complete();
                                return 0;
                            }
                            catch
                            {
                                transaction.Dispose();
                                return -2;
                            }
                        }
                    }
                }
            }
            return -1;
        }
    }
}
