using Asm1.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asm1.Data.Repository
{
    public sealed class CustomerDAO : ICustomer
    {
        private  FuminiHotelManagementContext _context;
       
        private CustomerDAO()
        {
        }
        private static CustomerDAO instance;
        public static CustomerDAO GetInstance()
        {
            if(instance == null)
            {
                 instance = new CustomerDAO();
            }
            return instance;
        }

        public CustomerDAO(FuminiHotelManagementContext context)
        {
            this._context = context;
        }

        public void CreateCustomer(Customer customer)
        {
            _context = new();
            _context.Add(customer);
            _context.SaveChanges();
        }

        public void DeleteCusById(int id)
        {
            _context = new();
         
            var CusToRemove = _context.Customers.FirstOrDefault(c => c.CustomerId == id);
            if (CusToRemove != null)
            {
                CusToRemove.CustomerStatus = 2;
                _context.SaveChanges();
            }

        }

        public IEnumerable<Customer> GetAll()
        {
            _context = new();
            return _context.Customers.Where(c => c.CustomerStatus != 2).ToList();
        }

        public Customer GetCustomerByMailNPass(string email, string password)
        {
            _context =new FuminiHotelManagementContext();
            return _context.Set<Customer>().Where(a => a.Password == password && a.EmailAddress == email && a.CustomerStatus!= 2).FirstOrDefault();
        }

        public IEnumerable<Customer> GetCustomerByName(string name)
        {
             _context= new();
            return _context.Customers.Where(c => c.CustomerFullName.Contains(name)).ToList();
        }

        public void UpdateCustomerbyId(Customer customer)
        {
            _context = new FuminiHotelManagementContext();

            _context.Customers.Update(customer);
            _context.SaveChanges();
        }
    }
}
