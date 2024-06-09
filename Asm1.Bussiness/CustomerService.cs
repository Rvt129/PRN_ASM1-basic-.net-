using Asm1.Data.Entities;
using Asm1.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asm1.Bussiness
{
    public sealed class CustomerService
    {
        private CustomerDAO customerDAO ;
        private static CustomerService instance ;

        public CustomerService()
        {
            customerDAO = CustomerDAO.GetInstance();
        }
       
        public static CustomerService GetInstance()
        {
            if (instance == null)
            {
                instance = new CustomerService();
            }
            return instance;
        }

        public IEnumerable<Customer> AllCustomerList()
        {
            return customerDAO.GetAll();
        }

        public void CreateCus(Customer customer)
        {
           customerDAO.CreateCustomer(customer);
        }

        public void DeleteCus(int id)
        {
            customerDAO.DeleteCusById(id);
        }

        public IEnumerable<Customer> SeacrhCusByName(string name)
        {
            return customerDAO.GetCustomerByName(name);
        }

        public void UpdateCustomer(Customer customer)
        {
            customerDAO.UpdateCustomerbyId(customer);
        }
    }
}
