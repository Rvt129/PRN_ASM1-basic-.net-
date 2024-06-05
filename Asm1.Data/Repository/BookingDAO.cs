using Asm1.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asm1.Data.Repository
{
    public sealed class BookingDAO : IBooking
    {
        private static readonly BookingDAO instance = new BookingDAO();
        private static FuminiHotelManagementContext _context;

        private BookingDAO()
        {
            _context = new FuminiHotelManagementContext();
        }

        public static BookingDAO Instance
        {
            get
            {
                return instance;
            }
        }


        public IEnumerable<BookingReservation> bookingReservations(int id)
        {
            _context = new();
            return _context.BookingReservations.Where(b => b.CustomerId == id).ToList();
        }

        public IEnumerable<BookingReservation> bookingReservationsByDate(DateTime? DateSearch)
        {
            _context = new();
            return _context.BookingReservations.Where(b => b.BookingDate == DateSearch);
        }
        public IEnumerable<BookingDetail> GetAllBooking()
        {
            _context = new();
            return _context.BookingDetails.OrderByDescending(b => b.StartDate).ToList();
        }
    }
}
