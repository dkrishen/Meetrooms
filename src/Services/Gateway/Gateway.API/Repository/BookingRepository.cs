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
            var data = new BookingTokenDto { Booking = booking, Token = token };

            return Rabbit.Publish("Booking", "Add", data);
        }

        public bool DeleteBooking(Guid id, string token)
        {
            var data = new GuidTokenDto { Id = id, Token = token };

            return Rabbit.Publish("Booking", "Delete", data);
        }

        public async Task<IEnumerable<Booking>> GetBookingsAsync(string token)
        {
            return await Request.Get
                .SendAsync<IEnumerable<Booking>>("api/Booking/AllBookings", token)
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Booking>> GetBookingsByUserAsync(Guid id, string token)
        {
            return await Request.Get
                .SendAsync<IEnumerable<Booking>>("api/Booking/BookingsByUserId", token, id)
                .ConfigureAwait(false);
        }

        public bool UpdateBooking(Booking booking, string token)
        {
            var data = new BookingTokenDto { Booking = booking, Token = token };

            return Rabbit.Publish("Booking", "Update", data);
        }
    }
}
