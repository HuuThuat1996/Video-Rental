using System.Collections.Generic;
using System.Linq;
using VideoRentalStoreSystem.DAL.DBContextEF;
using VideoRentalStoreSystem.DAL.Repositories;

namespace VideoRentalStoreSystem.BLL
{
    /// <summary>
    /// Indicate that the customer has unpaid late charges when the customer attempts to rent an item.
    /// This is done automatically when a customer’s id is entered.
    /// Re id customer
    /// Get list rental record details, which have late charge not yet payment
    /// if have late charge retur true else false
    /// and calculate total late charge
    /// </summary>
    public class CheckLateCharge
    {
        double totalLateCharge = 0;
        DBVRContext context;
        RentalRecordRepository rentalRecordRepository;
        RentalRecordDetailRepository detailRepository;
        public CheckLateCharge()
        {
            context = new DBVRContext();
            rentalRecordRepository = new RentalRecordRepository(context);
            detailRepository = new RentalRecordDetailRepository(context);
        }
        public double GetTotalLateCharge()
        {
            return totalLateCharge;
        }

        public bool Check(Customer customer)
        {
            SumLateCharge(customer.CustomerID);
            if (totalLateCharge > 0)
                return true;
            return false;
        }

        public bool Check(int customerId)
        {
            SumLateCharge(customerId);
            if (totalLateCharge > 0)
                return true;
            return false;
        }

        private void SumLateCharge(int customerId)
        {
            totalLateCharge = 0;
            List<RentalRecord> lstrentalRecord = rentalRecordRepository.GetByCustomer(customerId).ToList();
            if (lstrentalRecord != null)
            {
                foreach (RentalRecord rentalRecord in lstrentalRecord)
                {
                    totalLateCharge += rentalRecord.RentalRecordDetails.OrderBy(y => y.DateReturnActual).Where(x => x.LateCharge > 0).Sum(x => x.LateCharge).Value;
                }
            }
        }
    }
}