using MRA.Gateway.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MRA.Gateway.Repository
{
    public class BookingRepository : RepositoryBase , IBookingRepository
    {
        public BookingRepository(IConfiguration configuration) 
            : base(configuration.GetSection("MRA.Bookings").GetValue<string>("Url"))
        {
        }

        public bool AddBooking(Booking booking, string token)
        {
            return Request.Post.Send("api/Booking/AddBooking", token, booking);
        }

        public bool DeleteBooking(Guid id, string token)
        {
            return Request.Delete.Send("api/Booking/DeleteBooking", token, id);
        }

        public IEnumerable<Booking> GetBookings(string token)
        {
            return Request.Get.Send<IEnumerable<Booking>>("api/Booking/GetAllBookings", token);
        }

        public IEnumerable<Booking> GetBookingsByUser(Guid id, string token)
        {
            return Request.Get.Send<IEnumerable<Booking>>("api/Booking/GetBookingsByUserId", token, id);
        }

        public bool UpdateBooking(Booking booking, string token)
        {
            return Request.Put.Send("api/Booking/UpdateBooking", token, booking);
        }
    }
}
