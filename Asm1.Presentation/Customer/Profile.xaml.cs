using Asm1.Bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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

namespace Asm1.Presentation.Customer
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : Window
    {
        private static Data.Entities.Customer _customer;
        private CustomerService customerService;

        public Profile(Data.Entities.Customer customer)
        {
            InitializeComponent();
            _customer = customer;
            DataContext = _customer;


        }
    
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var tempCustomer = new Data.Entities.Customer
            {
                CustomerFullName = _customer.CustomerFullName,
                EmailAddress = _customer.EmailAddress,
                Telephone = _customer.Telephone,
                CustomerBirthday = _customer.CustomerBirthday,
                Password = _customer.Password
            };
            Popup_update.DataContext = tempCustomer;

            Popup_update.IsOpen = true;

        }

        private void Back_button(object sender, RoutedEventArgs e)
        {
            CustomerPage customerPage = new CustomerPage(_customer);
            customerPage.Show();
            this.Close();
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Lấy đối tượng tạm thời từ DataContext của Popup
            var tempCustomer = Popup_update.DataContext as Data.Entities.Customer;

            // Lấy giá trị từ các control
            string fullName = CustomerFullName.Text;
            string email = EmailAddress.Text;
            string telephone = Telephone.Text;
            DateTime? birthday = CustomerBirthday.SelectedDate;
            string password = Password.Text;

            // Kiểm tra các điều kiện
            bool isValid = true;

            // Kiểm tra các trường không được phép null
            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(telephone) || !birthday.HasValue || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                isValid = false;
            }

            // Kiểm tra telephone phải là số và độ dài <= 12
            if (!long.TryParse(telephone, out long telephoneNumber) || telephone.Length > 12)
            {
                MessageBox.Show("Số điện thoại không hợp lệ.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                isValid = false;
            }

            // Kiểm tra birthday phải ở quá khứ
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

                // Cập nhật đối tượng _customer với dữ liệu từ đối tượng tạm thời
                _customer.CustomerFullName = tempCustomer.CustomerFullName;
                _customer.EmailAddress = tempCustomer.EmailAddress;
                _customer.Telephone = tempCustomer.Telephone;
                _customer.CustomerBirthday = tempCustomer.CustomerBirthday;
                _customer.Password = tempCustomer.Password;

                
                Popup_update.IsOpen = false;

                customerService = CustomerService.Instance;
                customerService.UpdateCustomer(_customer);

                DataContext = null;
                DataContext = _customer;
            }
        }
    }
}
