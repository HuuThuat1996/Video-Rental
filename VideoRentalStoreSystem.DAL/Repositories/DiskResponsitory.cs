using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoRentalStoreSystem.DAL.DBContextEF;
using VideoRentalStoreSystem.DAL.Models;
namespace VideoRentalStoreSystem.DAL.Repositories
{
    public class DiskResponsitory
    {
        DBVRContext _context;
        public GenericRepository<Disk> genericDisk;

        public DiskResponsitory(DBVRContext context)
        {
            _context = context;
            genericDisk = new GenericRepository<Disk>(context);
        }
        public List<DiskInformation> GetInformationDisk(string title)
        {
            List<DiskInformation> lstDiskInformation = new List<DiskInformation>();
            var listDiskID = from disk in _context.Disks where disk.TitleDisk == title select disk;
            // var a = from b in listDiskID join j in _context.RentalRecordDetails on b.DiskID equals j.DiskID where 
            var listDiskOnshelf = listDiskID.Where(x => x.Status == "Trên kệ").ToList();

            var listInformationRental = from disk in listDiskID
                                        join rentalDetail in _context.RentalRecordDetails
                                        on disk.DiskID equals rentalDetail.DiskID
                                        where disk.Status == "Đã thuê"
                                        select new { disk.DiskID, rentalDetail.DateReturn, rentalDetail.RentalRecord.Customer.Name, disk.Status };
            var listnformationReservation = from disk in listDiskID
                                            join reservation in _context.Reservations
                                            on disk.TitleDisk equals reservation.TitleDisk
                                            where disk.Status == "Giữ lại"
                                            select new { disk.DiskID, reservation.Customer.Name, reservation.DateReservation, disk.Status };
            foreach (var item in listDiskOnshelf)
            {
                DiskInformation disk = new DiskInformation();
                disk.DiskID = item.DiskID;
                disk.Status = item.Status;
                lstDiskInformation.Add(disk);
            }
         
            foreach (var item in listInformationRental)
            {
               
                DiskInformation disk = new DiskInformation();
                disk.DiskID = item.DiskID;
                disk.CustomerName = item.Name;
                disk.DateReturn = item.DateReturn.ToString();
                disk.Status = item.Status;
                lstDiskInformation.Add(disk);

            }

            foreach (var item in listnformationReservation)
            {
                DiskInformation disk = new DiskInformation();
                disk.DiskID = item.DiskID;
                disk.CustomerName = item.Name;
                disk.DateResevartion = item.DateReservation.ToString();
                disk.Status = item.Status;
                lstDiskInformation.Add(disk);
            }
            // delete dounle item with ID in list
            for(int i = lstDiskInformation.Count-1; i>0;i--)
            {
                for(int j = lstDiskInformation.Count-1;j>0;j--)
                {
                    if(lstDiskInformation[i].DiskID == lstDiskInformation[j].DiskID
                        &&lstDiskInformation[i].Status == "Đã thuê")
                    {
                        if(DateTime.Parse(lstDiskInformation[i].DateReturn) > DateTime.Parse(lstDiskInformation[j].DateReturn))
                        {
                            lstDiskInformation.RemoveAt(j);
                        }
                    }
                    if (lstDiskInformation[i].DiskID == lstDiskInformation[j].DiskID
                       && lstDiskInformation[i].Status == "Giữ lại")
                    {
                        if (DateTime.Parse(lstDiskInformation[i].DateResevartion) > DateTime.Parse(lstDiskInformation[j].DateResevartion))
                        {
                            lstDiskInformation.RemoveAt(j);
                        }
                    }
                }
            }

            return lstDiskInformation;

        }
    }
}
