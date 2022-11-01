using MRA.Bookings.Models;
using MRA.Bookings.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MRA.Bookings.Logic
{
    public class BookingLogic : IBookingLogic
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingLogic(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<Booking> GetBookingByIdAsync(Guid bookingId)
        {
            return await _bookingRepository.GetBookingByIdAsync(bookingId);
        }

        public async Task<bool> AddBookingAsync(Booking booking)
        {
            if (await validateBooking(booking))
            {
                return await _bookingRepository.AddBookingAsync(booking);
            }

            return false;
        }

        public async Task<bool> UpdateBookingAsync(Booking booking)
        {
            if (await validateBooking(booking))
            {
                return await _bookingRepository.UpdateBookingAsync(booking);
            }

            return false;
        }

        public async Task<bool> DeleteBookingAsync(Guid id)
        {
            return await _bookingRepository.DeleteBookingAsync(id);
        }

        private async Task<bool> validateBooking(Booking booking)
        {
            var allBookings = await _bookingRepository.GetBookingsAsync();
            var data = allBookings;

            var collision = allBookings
                .Where(b => b.Date == booking.Date)
                .Where(b => {
                    if ((booking.StartTime > b.StartTime && booking.StartTime < b.EndTime) ||
                      (booking.EndTime > b.StartTime && booking.EndTime < b.EndTime) ||
                      (b.StartTime > booking.StartTime && b.StartTime < booking.EndTime))
                    {
                        return true;
                    }
                    else return false;
                }).ToList();

            var current = await _bookingRepository.GetBookingByIdAsync(booking.Id);

            if(current != null)
            {
                collision.Remove(current);
            }

            if (collision?.Count() != 0)
            {
                return false;
            }

            return true;
        }
    }
}
