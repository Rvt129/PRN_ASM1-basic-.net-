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
      
        private  FuminiHotelManagementContext _context;

        private BookingDAO()
        {
        }
        private static BookingDAO instance;
  
        public static BookingDAO GetInstance()
        {
            if (instance == null)
            {
                instance = new BookingDAO();
            }
            return instance;
        }
        public BookingDAO(FuminiHotelManagementContext context)
        {
            this._context = context;
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
