using System;
using System.Collections.Generic;
using VideoRentalStoreSystem.DAL.DBContextEF;
using VideoRentalStoreSystem.DAL.Repositories;

namespace VideoRentalStoreSystem.BLL
{
    public class ManageReservation
    {
        private ReservationRepository reservationRepository;
        public ManageReservation()
        {
            reservationRepository = new ReservationRepository(new DBVRContext());
        }
        public void AddReservation(Customer customer, TitleDisk titleDisk)
        {
            Reservation reservation = new Reservation();
            reservation.CustomerID = customer.CustomerID;
            reservation.DateReservation = DateTime.Now;
            reservation.Title = titleDisk.Title;
            reservationRepository.AddReservation(reservation);
        }
        public void DeleteReservation(Reservation reservation)
        {
            reservationRepository.DeleteReservation(reservation);
        }
        public void AddReservationReturnDisk(int DiskID)
        {
            reservationRepository.AddReservationReturnDisk(DiskID);
        }
    }
}
