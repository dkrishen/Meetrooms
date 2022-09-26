using MRA.Gateway.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MRA.Gateway.Repository
{
    public class BookingRepository : RepositoryBase , IBookingRepository
    {
        public BookingRepository(IConfiguration configuration) 
            : base(configuration.GetSection("MRA.Bookings").GetValue<string>("Url"))
        {
        }

        public async Task<bool> AddBookingAsync(Booking booking, string token)
        {
            return await Request.Post
                .SendAsync("api/Booking/AddBooking", token, booking)
                .ConfigureAwait(false);
        }

        public async Task<bool> DeleteBookingAsync(Guid id, string token)
        {
            return await Request.Delete
                .SendAsync("api/Booking/DeleteBooking", token, id)
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Booking>> GetBookingsAsync(string token)
        {
            return await Request.Get
                .SendAsync<IEnumerable<Booking>>("api/Booking/GetAllBookings", token)
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Booking>> GetBookingsByUserAsync(Guid id, string token)
        {
            return await Request.Get
                .SendAsync<IEnumerable<Booking>>("api/Booking/GetBookingsByUserId", token, id)
                .ConfigureAwait(false);
        }

        public async Task<bool> UpdateBookingAsync(Booking booking, string token)
        {
            return await Request.Put
                .SendAsync("api/Booking/UpdateBooking", token, booking)
                .ConfigureAwait(false);
        }
    }
}
