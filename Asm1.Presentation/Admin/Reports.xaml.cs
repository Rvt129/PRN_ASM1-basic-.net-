using Asm1.Bussiness;
using Asm1.Data.Entities;
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
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class Reports : Window
    {
        private BookingService bookingService;

        public IEnumerable<BookingDetail> BookingReport()
        {
            return bookingService.GetBookingReport();
        }
        public Reports()
        {
            InitializeComponent();
            bookingService = BookingService.Instance;
            lvi.ItemsSource = BookingReport();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            AdminPage adminPage = new AdminPage();
            adminPage.Show();
            this.Close();
        }
    }
}
