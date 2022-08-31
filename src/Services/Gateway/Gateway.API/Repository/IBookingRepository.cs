using MRA.Gateway.Models;
using System;
using System.Collections.Generic;

namespace MRA.Gateway.Repository
{
    public interface IBookingRepository
    {
        public IEnumerable<Booking> GetBookings(string token);
        public IEnumerable<Booking> GetBookingsByUser(Guid id, string token);
        public void AddBooking(Booking booking, string token);
        public void UpdateBooking(Booking booking, string token);
        public void DeleteBooking(Guid id, string token);
    }
}
