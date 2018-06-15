namespace VideoRentalStoreSystem.DAL.DBContextEF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RentalRecordDetail
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RentalRecordID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DiskID { get; set; }

        public DateTime DateReturn { get; set; }

        public DateTime? DateReturnActual { get; set; }

        public double PriceRental { get; set; }

        public double? LateCharge { get; set; }

        public virtual Disk Disk { get; set; }

        public virtual RentalRecord RentalRecord { get; set; }
    }
}
