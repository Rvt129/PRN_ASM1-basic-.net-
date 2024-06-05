using Asm1.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asm1.Data.Repository
{
    public interface ICustomer
    {
        public Customer GetCustomerByMailNPass(string email,string password);

        public void UpdateCustomerbyId(Customer customer);
        public IEnumerable<Customer> GetAll();
        public void CreateCustomer(Customer customer);
        public void DeleteCusById(int id);
        public IEnumerable<Customer> GetCustomerByName(string name);

    }
}
