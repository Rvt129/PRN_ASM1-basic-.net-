using Asm1.Presentation.Customer;
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

namespace Asm1.Presentation.Admin
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Window
    {
        public AdminPage()
        {
            InitializeComponent();
        }

        private void customer_Click(object sender, RoutedEventArgs e)
        {
            CustomerList customerList =new CustomerList();
            customerList.Show();
            this.Close();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Login homepage = new Login();
            homepage.Show();
            this.Close();
        }



        private void Room_click(object sender, RoutedEventArgs e)
        {
            RoomList roomList = new RoomList();
            roomList.Show();
            this.Close();
        }

        private void Report_click(object sender, RoutedEventArgs e)
        {
            Reports report = new Reports();
            report.Show();
            this.Close();
        }
    }
}
