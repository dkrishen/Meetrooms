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
        public Task<bool> AddBookingAsync(Booking booking, string token);
        public Task<bool> UpdateBookingAsync(Booking booking, string token);
        public Task<bool> DeleteBookingAsync(Guid id, string token);
    }
}
