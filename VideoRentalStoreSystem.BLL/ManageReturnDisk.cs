using System;
using System.Collections.Generic;
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
        private RentalRecordRepository recordRepository;
        private ReservationRepository reservationRepository;
        public ManageReturnDisk( )
        {
            detailRepository = new RentalRecordDetailRepository(new DBVRContext());
            diskRepository = new DiskRepository(new DBVRContext());
            recordRepository = new RentalRecordRepository(new DBVRContext());
            reservationRepository = new ReservationRepository(new DBVRContext());
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
        public int ReturnDisk(Disk disk, DateTime dateReturn)
        {
            if (disk != null)
            {
                AddLateCharge addLateCharge = new AddLateCharge();
                RentalRecordDetail detailLatest = detailRepository.GetLatest(disk.DiskID);
                RentalRecord rentalRecord = recordRepository.Get(detailLatest.RentalRecordID);

                if (detailLatest != null)
                {
                    if (dateReturn > detailLatest.DateReturn.Date)
                    {
                        int result = addLateCharge.Add(disk.DiskID, dateReturn);
                        if (result >= 0)
                        {
                            return 1;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else if(dateReturn < rentalRecord.DateRental)
                    {
                        return 3;
                    }
                    else
                    {
                        using (TransactionScope transaction = new TransactionScope())
                        {
                            try
                            {
                                detailLatest.DateReturnActual = dateReturn;
                                detailRepository.Update(detailLatest);
                                diskRepository.ModifyStatus(disk, StatusOfDisk.ON_SHELF);
                                //reservationRepository.AddReservationReturnDisk(disk.DiskID);
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
        public void  UpdateLateCharge(RentalRecordDetail detail)
        {
            detailRepository.UpdateLateCharge(detail);
          
        }
       
       

    }
}
