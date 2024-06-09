using Asm1.Bussiness;
using Asm1.Data.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for HistoryBook.xaml
    /// </summary>
    public partial class HistoryBook : Window
    {
        private static Data.Entities.Customer _customer;
        private BookingService _bookingService;
        private ObservableCollection<BookingReservation> _reservations = new ObservableCollection<BookingReservation>();


        public HistoryBook(Data.Entities.Customer customer)
        {
            InitializeComponent();
            _customer = customer;
            lvi.ItemsSource = BookingReservations();
        }
        public IEnumerable<BookingReservation> BookingReservations()
        {
            _bookingService = BookingService.GetInstance();
            return _bookingService.GetReservationById(_customer.CustomerId);
        }

        private void Back_click(object sender, RoutedEventArgs e)
        {
            CustomerPage customerPage = new CustomerPage(_customer);
            customerPage.Show();
            this.Close();   
        }

        private void Search_button_Click(object sender, RoutedEventArgs e)
        {
            if (DateSearch.SelectedDate != null)
            {
                DateTime? datesearch = DateSearch.SelectedDate;
                SearchbookingReservations(datesearch);
                lvi.ItemsSource = _reservations;
            }
        }

        public void SearchbookingReservations(DateTime? DateSearch)
        {
            _bookingService = BookingService.GetInstance();
            var reservations = _bookingService.GetReservationByDate(DateSearch);
            _reservations.Clear();
            foreach (var reservation in reservations)
            {
                _reservations.Add(reservation);
            }
        }
    }
}
