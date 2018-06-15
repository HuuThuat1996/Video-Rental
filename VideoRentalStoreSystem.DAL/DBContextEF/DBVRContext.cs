namespace VideoRentalStoreSystem.DAL.DBContextEF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBVRContext : DbContext
    {
        public DBVRContext()
            : base("name=DBVRContext")
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Disk> Disks { get; set; }
        public virtual DbSet<RentalRecord> RentalRecords { get; set; }
        public virtual DbSet<RentalRecordDetail> RentalRecordDetails { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<TitleDisk> TitleDisks { get; set; }
        public virtual DbSet<TypeDisk> TypeDisks { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(e => e.PhoneNumber)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsFixedLength();
        }
    }
}
