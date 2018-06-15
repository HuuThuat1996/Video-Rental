namespace VideoRentalStoreSystem.UI.Models
{
    public class CustomerModel
    {
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int IsDeleted { get; set; }
    }
}
