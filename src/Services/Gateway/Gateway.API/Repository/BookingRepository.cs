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
            : base(configuration.GetSection("MRA.Bookings").GetValue<string>("Url"), configuration)
        {
        }

        public bool AddBooking(Booking booking, string token)
        {
            return Rabbit.Publish("Booking", "Add", booking);
        }

        public bool DeleteBooking(Guid id, string token)
        {
            return Rabbit.Publish("Booking", "Delete", id);
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

        public bool UpdateBooking(Booking booking, string token)
        {
            return Rabbit.Publish("Booking", "Update", booking);
        }
    }
}
