using VideoRentalStoreSystem.DAL.DBContextEF;
using VideoRentalStoreSystem.DAL.Repositories;

namespace VideoRentalStoreSystem.BLL
{
    public class ManageCustomer
    {
        private CustomerRepository customerRepository;

        public ManageCustomer()
        {
            customerRepository = new CustomerRepository(new DBVRContext());
        }

        public void AddCustomer(Customer customer)
        {
            customerRepository.Insert(customer);
        }
    }
}
