using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoRentalStoreSystem.DAL
{
   public class StatusCustomer
    {
        public static int Deleted = 1;
        public static int Already = 0;
        public static string DiskLate = "Chưa trả đĩa";
        public static string LateCharge = "Nợ phí trễ hạn";
       
    }
}
