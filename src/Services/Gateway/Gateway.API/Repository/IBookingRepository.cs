using MRA.Gateway.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MRA.Gateway.Repository
{
    public interface IBookingRepository
    {
        public Task<IEnumerable<Booking>> GetBookingsAsync(string token);
        public Task<IEnumerable<Booking>> GetBookingsByUserAsync(Guid id, string token);
        public bool AddBooking(Booking booking, string token);
        public bool UpdateBooking(Booking booking, string token);
        public bool DeleteBooking(Guid id, string token);
    }
}
