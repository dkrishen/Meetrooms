using Microsoft.EntityFrameworkCore;
using MRA.Bookings.Data;
using MRA.Bookings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRA.Bookings.Repositories
{
    public class BookingRepository : RepositoryBase, IBookingRepository
    {
        public BookingRepository(MRABookingsDbContext context) : base(context) { }

        public async Task AddBookingAsync(Booking booking)
        {
            await context.Bookings.AddAsync(booking).ConfigureAwait(false);
            await context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteBookingAsync(Guid id)
        {
            Booking booking = await context.Bookings
                .Where(b => b.Id == id)
                .SingleOrDefaultAsync().ConfigureAwait(false);

            context.Bookings.Remove(booking);
            await context.SaveChangesAsync().ConfigureAwait(false); 
        }

        public async Task<Booking> GetBookingByIdAsync(Guid bookingId)
        {
            return await context.Bookings
                .Where(b => b.Id == bookingId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Booking>> GetBookingsAsync()
        {
            return await context.Bookings
                .ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<Booking>> GetBookingsByUserAsync(Guid userId)
        {
            return await context.Bookings
                .Where(b => b.UserId == userId)
                .ToListAsync().ConfigureAwait(false); ;
        }

        public async Task UpdateBookingAsync(Booking booking)
        {
            var result = await context.Bookings
                .SingleOrDefaultAsync(b => b.Id == booking.Id).ConfigureAwait(false); 

            if (result != null)
            {
                result.StartTime = booking.StartTime;
                result.EndTime = booking.EndTime;
                result.Date = booking.Date;
                await context.SaveChangesAsync().ConfigureAwait(false); ;
            }
        }
    }
}
