using Asm1.Data.Entities;
using Asm1.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asm1.Bussiness
{
    public sealed class BookingService
    {
        private static readonly BookingService instance = new BookingService();
        private BookingDAO bookingDAO;

        private BookingService()
        {
            bookingDAO = BookingDAO.Instance;
        }

        public static BookingService Instance
        {
            get
            {
                return instance;
            }
        }

        public IEnumerable<BookingDetail> GetBookingReport()
        {
            return bookingDAO.GetAllBooking();
        }

        public IEnumerable<BookingReservation> GetReservationByDate(DateTime? DateSearch)
        {
            return bookingDAO.bookingReservationsByDate(DateSearch);
        }

        public IEnumerable<BookingReservation> GetReservationById(int customerId)
        {
            return bookingDAO.bookingReservations(customerId);
        }
    }
}
