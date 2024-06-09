using Asm1.Bussiness;
using Asm1.Data.Entities;
using Asm1.Data.Repository;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Asm1.Presentation.Admin
{
    /// <summary>
    /// Interaction logic for CustomerList.xaml
    /// </summary>
    public partial class CustomerList : Window
    {
        private CustomerService customerService;
        private static Data.Entities.Customer _selectedCustomer;
        public CustomerList()
        {
            InitializeComponent();

            lvi.ItemsSource = CusList();
        }
        private IEnumerable<Data.Entities.Customer> CusList()
        {
            customerService = CustomerService.GetInstance();
            return customerService.AllCustomerList();
        }

        private void Update_Customer(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                _selectedCustomer = button.CommandParameter as Data.Entities.Customer;
                if (_selectedCustomer != null)
                {
                    var tempCustomer = new Data.Entities.Customer
                    {
                        CustomerFullName = _selectedCustomer.CustomerFullName,
                        EmailAddress = _selectedCustomer.EmailAddress,
                        Telephone = _selectedCustomer.Telephone,
                        CustomerBirthday = _selectedCustomer.CustomerBirthday,
                        Password = _selectedCustomer.Password,
                        CustomerStatus = _selectedCustomer.CustomerStatus
                    };

                    Popup_update.DataContext = tempCustomer;
                    
                    Popup_update.IsOpen = true;
                }
            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Lấy đối tượng tạm thời từ DataContext của Popup
            var tempCustomer = Popup_update.DataContext as Data.Entities.Customer;

            string fullName = CustomerFullName.Text;
            string email = EmailAddress.Text;
            string telephone = Telephone.Text;
            DateTime? birthday = CustomerBirthday.SelectedDate;
            string password = Password.Text;
          
            
                bool isValid = true;

            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(telephone) || !birthday.HasValue || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                isValid = false;
            }


            if (birthday > DateTime.Now)
            {
                MessageBox.Show("Ngày sinh phải ở quá khứ.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                isValid = false;
            }

            if (isValid)
            {
             
                tempCustomer.CustomerFullName = fullName;
                tempCustomer.EmailAddress = email;
                tempCustomer.Telephone = telephone;
                tempCustomer.CustomerBirthday = birthday.Value;
                tempCustomer.Password = password;
                
                // Cập nhật đối tượng _selectedCustomer với dữ liệu từ đối tượng tạm thời
                _selectedCustomer.CustomerFullName = tempCustomer.CustomerFullName;
                _selectedCustomer.EmailAddress = tempCustomer.EmailAddress;
                _selectedCustomer.Telephone = tempCustomer.Telephone;
                _selectedCustomer.CustomerBirthday = tempCustomer.CustomerBirthday;
                _selectedCustomer.Password = tempCustomer.Password;
                
                
                Popup_update.IsOpen = false;

                customerService = CustomerService.GetInstance();
                customerService.UpdateCustomer(_selectedCustomer);

                DataContext = null;
                DataContext = _selectedCustomer;

                lvi.Items.Refresh();
            }
        }

        private void Delete_Customer(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this customer?", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                Button button = sender as Button;
                if (button != null)
                {
                    var customer = button.CommandParameter as Data.Entities.Customer;
                    if (customer != null)
                    {
                        DeleteCustomer(customer.CustomerId);
                    }
                }
            }
        }

        private void DeleteCustomer(int customerId)
        {
            customerService = CustomerService.GetInstance();
            customerService.DeleteCus(customerId);

            lvi.ItemsSource=CusList();
        }

       
        private void Search_button_Click(object sender, RoutedEventArgs e)
        {

            if(!search.Text.IsNullOrEmpty())
            {
                lvi.ItemsSource = SearchCusList(search.Text);

            }
            else if (search.Text == "") lvi.ItemsSource = lvi.ItemsSource;
        }
        private IEnumerable<Data.Entities.Customer> SearchCusList(String name)
        {
            customerService = CustomerService.GetInstance();
            return customerService.SeacrhCusByName(name);
        }
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            Popup_create.IsOpen = true;

        }

        private void Back_click(object sender, RoutedEventArgs e)
        {
            AdminPage adminPage = new AdminPage();
            adminPage.Show();
            this.Close();
        }

        private void SaveButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            if(name != null && tele!= null && email != null && birthday.SelectedDate < DateTime.Today)
            {
                Data.Entities.Customer customer = new Data.Entities.Customer
                {
                    CustomerFullName = name.Text,
                    CustomerBirthday = (DateTime)birthday.SelectedDate,
                    EmailAddress = email.Text,
                    Telephone = tele.Text,
                    CustomerStatus=1,
                    Password="12345678"
                };
                customerService.CreateCus(customer);
                Popup_create.IsOpen=false;
                lvi.ItemsSource=CusList();
            }
            else
            {
                MessageBox.Show("Incorrect input details");
                return;
            }
           

        }

        private void PrieviewTeleEdit(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            if (regex.IsMatch(e.Text))
            {
                e.Handled = true;
                return;
            }

            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Text.Length >= 12)
            {
                e.Handled = true;
            }
        }

        private void PreviewTeleAdd(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            if (regex.IsMatch(e.Text))
            {
                e.Handled = true;
                return;
            }

            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Text.Length >= 12)
            {
                e.Handled = true;
            }
        }
    }
}
