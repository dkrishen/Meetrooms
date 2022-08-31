using MRA.Gateway.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MRA.Gateway.Repository
{
    public class BookingRepository : RepositoryBase , IBookingRepository
    {
        public BookingRepository(IConfiguration configuration) : base(configuration.GetSection("MRA.Bookings").GetValue<string>("Url"))
        {
        }

        public void AddBooking(Booking booking, string token)
        {
            Request("api/Booking/AddBooking?data=", "POST", booking, token);
        }

        public void DeleteBooking(Guid id, string token)
        {
            Request("api/Booking/DeleteBooking?data=", "DELETE", id, token);
        }

        public IEnumerable<Booking> GetBookings(string token)
        {
            var jsonResponse = Request("api/Booking/GetAllBookings", "GET", token: token);
            return JsonConvert.DeserializeObject<IEnumerable<Booking>>(jsonResponse);
        }

        public IEnumerable<Booking> GetBookingsByUser(Guid id, string token)
        {
            var jsonResponse = Request("api/Booking/GetBookingsByUserId?data=", "GET", id, token);
            return JsonConvert.DeserializeObject<IEnumerable<Booking>>(jsonResponse);
        }

        public void UpdateBooking(Booking booking, string token)
        {
            Request("api/Booking/UpdateBooking?data=", "PUT", booking, token);
        }
    }
}
