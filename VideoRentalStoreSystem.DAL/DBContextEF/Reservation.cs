namespace VideoRentalStoreSystem.DAL.DBContextEF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Reservation")]
    public partial class Reservation
    {
        public int ReservationID { get; set; }

        public DateTime DateReservation { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        public int? DiskID { get; set; }

        public int CustomerID { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Disk Disk { get; set; }

        public virtual TitleDisk TitleDisk { get; set; }
    }
}
