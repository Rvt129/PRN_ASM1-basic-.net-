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

namespace Asm1.Presentation.Customer
{
    /// <summary>
    /// Interaction logic for CustomerPage.xaml
    /// </summary>
    public partial class CustomerPage : Window
    {
        private static Data.Entities.Customer _customer;

        public CustomerPage(Data.Entities.Customer customer)
        {
            InitializeComponent();
            _customer = customer;
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            Profile profile = new Profile(_customer);
            profile.Show();
            this.Close();
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            Login homepage = new Login();
            homepage.Show();
            this.Close();
        }

        private void History_click(object sender, RoutedEventArgs e)
        {
            HistoryBook historyBook = new HistoryBook(_customer);
            historyBook.Show();
            this.Close();
        }
    }
}
