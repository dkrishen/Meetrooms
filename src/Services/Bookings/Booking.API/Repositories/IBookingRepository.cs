using MRA.Bookings.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MRA.Bookings.Repositories
{
    public interface IBookingRepository
    {
        public Task<IEnumerable<Booking>> GetBookingsAsync();
        public Task<IEnumerable<Booking>> GetBookingsByUserAsync(Guid userId);
        public Task<Booking> GetBookingByIdAsync(Guid bookingId);
        public Task AddBookingAsync(Booking booking);
        public Task UpdateBookingAsync(Booking booking);
        public Task DeleteBookingAsync(Guid id);
    }
}
