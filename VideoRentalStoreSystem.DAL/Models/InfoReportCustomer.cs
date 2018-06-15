using System;

namespace VideoRentalStoreSystem.DAL.Models
{
    public class InfoReportCustomer
    {
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int DiskID { get; set; }
        public string Title { get; set; }
        public DateTime DateReturn { get; set; }
    }
}
