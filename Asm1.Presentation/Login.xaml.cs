using Asm1.Bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.IO;
using Asm1.Presentation.Admin;
using Asm1.Presentation.Customer;
using Asm1.Data.Entities;
namespace Asm1.Presentation
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private LoginService _service;
        private string configFilePath = "G:\\fu\\kif 5\\prrn\\Asm1\\Asm1.Presentation\\appsetting.json";
        private string adminEmail;
        private string adminPassword;
        
        public Login()
        {
            InitializeComponent();
            LoadAdminCredentials();
        }

        private void LoadAdminCredentials()
        {
            try
            {
                Configuration config = ConfigLoader.LoadConfig(configFilePath); 
                adminEmail = config.AdminAccount.Email;
                adminPassword = config.AdminAccount.Password;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load configuration: " + ex.Message);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String emailuser = email.Text;
            String passworduser= password.Text;
            if(emailuser == null ||  passworduser == null)
            {
                MessageBox.Show("You have to input email and password to login");
            }
            else if(emailuser.Equals(adminEmail) && adminPassword.Equals(adminPassword))
            {
                AdminPage adminPage = new AdminPage();
                adminPage.Show();
                this.Close();
            }
            else
            {
     
                _service = LoginService.Instance;
                if(_service.LoginUser(emailuser, passworduser))
                {
                    Data.Entities.Customer customer = _service.User(emailuser, passworduser);
                    CustomerPage customerPage = new CustomerPage(customer); 
                    customerPage.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Incorrect email or password");
                }
                
            }
            

        }
    }
}
