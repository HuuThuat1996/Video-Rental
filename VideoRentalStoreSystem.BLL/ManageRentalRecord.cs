using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using VideoRentalStoreSystem.DAL;
using VideoRentalStoreSystem.DAL.DBContextEF;
using VideoRentalStoreSystem.DAL.Repositories;

namespace VideoRentalStoreSystem.BLL
{
    public class ManageRentalRecord
    {
        RentalRecordRepository rentalRepository;
        DiskRepository diskRepository;

        public ManageRentalRecord(DBVRContext context)
        {
            rentalRepository = new RentalRecordRepository(context);
            diskRepository = new DiskRepository(context);
        }

        /// <summary>
        /// Initialize RentalRecord:
        /// Calculate the total payment,
        /// add customerId
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="details"></param>
        /// <returns>RentalRecord</returns>
        public RentalRecord Initialize(int customerId, List<RentalRecordDetail> details)
        {
            RentalRecord rentalRecord = new RentalRecord();
            rentalRecord.CustomerID = customerId;
            rentalRecord.DateRental = DateTime.Now;
            rentalRecord.RentalRecordDetails = details;
            rentalRecord.TotalPrice = CalculateTotalPayment(details);
            return rentalRecord;
        }

        /// <summary>
        /// Calculate total payment when rental disk 
        /// </summary>
        /// <param name="lst"></param>
        /// <returns></returns>
        public double CalculateTotalPayment(List<RentalRecordDetail> lst)
        {
            double total = 0;
            if (lst != null)
            {
                foreach (RentalRecordDetail item in lst)
                {
                    total += item.PriceRental;
                }
            }
            return total;
        }

        /// <summary>
        /// Add a RentalRecord and change status of disk rented
        /// </summary>
        /// <param name="rentalRecord"></param>
        /// <returns></returns>
        public bool AddRentalRecord(RentalRecord rentalRecord)
        {
            if (rentalRecord != null)
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {
                        rentalRepository.Insert(rentalRecord);
                        diskRepository.ModifyStatus(rentalRecord.RentalRecordDetails.Select(x => x.DiskID).ToList(), StatusOfDisk.RENTED);
                        transaction.Complete();
                        return true;
                    }
                    catch
                    {
                        transaction.Dispose();
                        return false;
                    }
                }
            }
            return false;
        }
    }
}
