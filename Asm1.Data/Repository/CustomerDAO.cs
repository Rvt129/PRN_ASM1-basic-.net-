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
        private static readonly CustomerDAO instance = new CustomerDAO();
        public CustomerDAO()
        {
        }
        public static CustomerDAO Instance
        {
            get
            {
                return instance;
            }
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
                CusToRemove.CustomerStatus = 0;
                _context.SaveChanges();
            }

        }

        public IEnumerable<Customer> GetAll()
        {
            _context = new();
            return _context.Customers.Where(c => c.CustomerStatus != 0).ToList();
        }

        public Customer GetCustomerByMailNPass(string email, string password)
        {
            _context =new FuminiHotelManagementContext();
            return _context.Set<Customer>().Where(a => a.Password == password && a.EmailAddress == email).FirstOrDefault();
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
