using MRA.Bookings.Models;
using System;
using System.Collections.Generic;

namespace MRA.Bookings.Repositories
{
    public interface IBookingRepository
    {
        public IEnumerable<Booking> GetBookings();
        public IEnumerable<Booking> GetBookingsByUser(Guid userId);
        public void AddBooking(Booking booking);
        public void UpdateBooking(Booking booking);
        public void DeleteBooking(Guid id);
    }
}
