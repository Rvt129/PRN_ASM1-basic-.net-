using Asm1.Data.Entities;
using Asm1.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asm1.Bussiness
{
    public sealed class LoginService
    {
        private CustomerDAO customerDAO;
        private static LoginService instance;

        public LoginService()
        {
            customerDAO = CustomerDAO.GetInstance();
        }
     
 
        public static LoginService GetInstance()
        {
            if (instance == null)
            {
                instance = new LoginService();
            }
            return instance;
        }

        public bool LoginUser(string emailuser, string passworduser)
        {
            Customer customer=customerDAO.GetCustomerByMailNPass(emailuser, passworduser);
           if(customer == null ) {return false;}else{return true;}
        }
        public Customer User(string emailuser, string passworduser)
        {
            return customerDAO.GetCustomerByMailNPass(emailuser, passworduser);
        }
    }
}
