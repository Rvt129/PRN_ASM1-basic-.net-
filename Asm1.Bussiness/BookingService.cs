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
    
        private BookingDAO bookingDAO;
        private static BookingService instance;
        private BookingService()
        {
            bookingDAO = BookingDAO.GetInstance();
        }

        public static BookingService GetInstance()
        {
            if (instance == null)
            {
                instance = new BookingService();
            }
            return instance;
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
