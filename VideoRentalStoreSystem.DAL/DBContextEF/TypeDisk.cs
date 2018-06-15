namespace VideoRentalStoreSystem.DAL.DBContextEF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TypeDisk")]
    public partial class TypeDisk
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TypeDisk()
        {
            TitleDisks = new HashSet<TitleDisk>();
        }

        [Key]
        [StringLength(255)]
        public string TypeName { get; set; }

        public int Period { get; set; }

        public double Cost { get; set; }

        public double LateCharge { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TitleDisk> TitleDisks { get; set; }
    }
}
