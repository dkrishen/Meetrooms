using MRA.Bookings.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MRA.Bookings.Repositories
{
    public interface IBookingRepository
    {
        public Task<IEnumerable<Booking>> GetBookingsAsync();
        public Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(Guid userId);
        public Task<Booking> GetBookingByIdAsync(Guid bookingId);
        public Task<bool> AddBookingAsync(Booking booking);
        public Task<bool> UpdateBookingAsync(Booking booking);
        public Task<bool> DeleteBookingAsync(Guid id);
    }
}
