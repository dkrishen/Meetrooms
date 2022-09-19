using MRA.Gateway.Models;
using System;
using System.Collections.Generic;

namespace MRA.Gateway.Repository
{
    public interface IBookingRepository
    {
        public IEnumerable<Booking> GetBookings(string token);
        public IEnumerable<Booking> GetBookingsByUser(Guid id, string token);
        public bool AddBooking(Booking booking, string token);
        public bool UpdateBooking(Booking booking, string token);
        public bool DeleteBooking(Guid id, string token);
    }
}
