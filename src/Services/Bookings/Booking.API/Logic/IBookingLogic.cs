using MRA.Bookings.Models;
using System;
using System.Threading.Tasks;

namespace MRA.Bookings.Logic
{
    public interface IBookingLogic
    {
        public Task<bool> AddBookingAsync(Booking booking);
        public Task<bool> UpdateBookingAsync(Booking booking);
        public Task<bool> DeleteBookingAsync(Guid id);
        public Task<Booking> GetBookingByIdAsync(Guid bookingId);
    }
}
