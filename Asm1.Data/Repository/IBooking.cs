using Asm1.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asm1.Data.Repository
{
    public interface IBooking
    {
        IEnumerable<BookingReservation> bookingReservations(int id);
        IEnumerable<BookingDetail> GetAllBooking();
        IEnumerable<BookingReservation> bookingReservationsByDate(DateTime? datesearch);
    }
}
