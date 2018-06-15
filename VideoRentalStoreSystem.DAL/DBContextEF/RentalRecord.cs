namespace VideoRentalStoreSystem.DAL.DBContextEF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RentalRecord")]
    public partial class RentalRecord
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RentalRecord()
        {
            RentalRecordDetails = new HashSet<RentalRecordDetail>();
        }

        public int RentalRecordID { get; set; }

        public DateTime DateRental { get; set; }

        public double TotalPrice { get; set; }

        public int CustomerID { get; set; }

        public virtual Customer Customer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RentalRecordDetail> RentalRecordDetails { get; set; }
    }
}
