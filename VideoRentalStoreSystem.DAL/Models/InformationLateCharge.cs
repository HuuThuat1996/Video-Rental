using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoRentalStoreSystem.DAL.Models
{
    public class InformationLateCharge
    {
        public int DiskID { get; set; }
        public string Title { get; set; }
        public DateTime DateRental { get; set; }
        public DateTime? DateReturn { get; set; }
        public DateTime? DateReturnActual { get; set; }
        public int LateCharge { get; set; }
    }
}
