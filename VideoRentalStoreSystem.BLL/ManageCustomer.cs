using System.Collections.Generic;
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
        public string isDelete(int customerID)
        {
            List<string> list = new List<string>();
            string alert = "";
            list = customerRepository.IsDelete(customerID);
            if (list.Count != 0)
            {
                foreach (string text in list)
                {
                    alert += " "+text;
                }
            }
            return alert;
        }
        public void DeleteCustomer(int customerID)
        {
            customerRepository.DeleteCustomer(customerID);
        }

    }

}
