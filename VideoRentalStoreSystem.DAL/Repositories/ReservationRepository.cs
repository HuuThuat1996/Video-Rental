using System;
using System.Collections.Generic;
using System.Linq;
using VideoRentalStoreSystem.DAL.DBContextEF;
using VideoRentalStoreSystem.DAL.Interfaces;


namespace VideoRentalStoreSystem.DAL.Repositories
{
    public class ReservationRepository : GenericRepository<DBVRContext, Reservation>
    {
        public ReservationRepository(DBVRContext context) : base(context)
        {
        }
        public void AddReservation(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
        }
        public void DeleteReservation(Reservation reservation)
        {
             
            Reservation reser = _context.Reservations.Where(x => x.ReservationID == reservation.ReservationID).FirstOrDefault();
            _context.Reservations.Remove(reser);
            _context.SaveChanges();
        }

        public List<Reservation> GetListReservation()
        {
            return _context.Reservations.ToList();
        }

        public void AddReservationReturnDisk(int? diskID)
        {
            Disk disk = new Disk();
            disk = _context.Disks.Where(x => x.DiskID == diskID).FirstOrDefault();
            Reservation reservation = GetReservationLatest(disk.Title);
            reservation.DiskID = diskID;
            disk.Status = StatusOfDisk.ON_HOLD;
            _context.SaveChanges();
        }
        public Reservation GetReservationLatest(string title)
        {
            Reservation reservation = new Reservation();
            foreach (Reservation reser in _context.Reservations.Where(x => x.Title == title && x.DiskID == null ).ToList())
            {
                if (reservation.CustomerID == 0)
                    reservation = reser;
                else
                {
                    if (reservation.DateReservation > reser.DateReservation)
                        reservation = reser;
                }
            }
            return reservation;
        }
    }
}
