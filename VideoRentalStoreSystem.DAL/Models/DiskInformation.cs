using System;

namespace VideoRentalStoreSystem.DAL.Models
{
    public class DiskInformation
    {
        public int DiskID { get; set; }
        public string Status { get; set; }
        public string CustomerName { get; set; }
        public DateTime? DateResevartion { get; set; }
        public DateTime? DateReturn { get; set; }
    }
}
