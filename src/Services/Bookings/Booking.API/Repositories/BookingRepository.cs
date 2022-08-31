using MRA.Bookings.Data;
using MRA.Bookings.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MRA.Bookings.Repositories
{
    public class BookingRepository : RepositoryBase, IBookingRepository
    {
        public BookingRepository(MRABookingsDbContext context) : base(context) { }

        public void AddBooking(Booking booking)
        {
            context.Bookings.Add(booking);
            context.SaveChanges();
        }

        public void DeleteBooking(Guid id)
        {
            Booking booking = context.Bookings.Where(b => b.Id == id).SingleOrDefault();
            context.Bookings.Remove(booking);
            context.SaveChanges();
        }

        public IEnumerable<Booking> GetBookings()
        {
            return context.Bookings.ToList();
        }

        public IEnumerable<Booking> GetBookingsByUser(Guid userId)
        {
            return context.Bookings.Where(b => b.UserId == userId).ToList();
        }

        public void UpdateBooking(Booking booking)
        {
            var result = context.Bookings.SingleOrDefault(b => b.Id == booking.Id);
            if (result != null)
            {
                result.StartTime = booking.StartTime;
                result.EndTime = booking.EndTime;
                result.Date = booking.Date;
                context.SaveChanges();
            }
        }
    }
}
