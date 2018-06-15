using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoRentalStoreSystem.DAL.DBContextEF
{
    public class DiskInformation
    {
        public int DiskID { get; set; }
        public string Status { get; set; }
        public string CustomerName { get; set; }
        public string DateResevartion { get; set; }
        public string DateReturn { get; set; }
    }
}
