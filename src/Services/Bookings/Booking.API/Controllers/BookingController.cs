using MRA.Bookings.Models;
using MRA.Bookings.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

namespace MRA.Bookings.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly ILogger<BookingController> _logger;
        private readonly IBookingRepository _bookingRepository;


        public BookingController(ILogger<BookingController> logger, IBookingRepository bookingRepository)
        {
            _logger = logger;
            _bookingRepository = bookingRepository;
        }

        [HttpGet]
        [Route("GetAllBookings")]
        public IActionResult GetBookings()
        {
            var bookings = _bookingRepository.GetBookings();
            var response = JsonConvert.SerializeObject(bookings);
            return Ok(response);
        }

        [HttpGet]
        [Route("GetBookingsByUserId")]
        public IActionResult GetBookingsByUserId(string data)
        {
            Guid userId = JsonConvert.DeserializeObject<Guid>(data);
            return Ok(JsonConvert.SerializeObject(_bookingRepository.GetBookingsByUser(userId)));
        }

        [HttpPost]
        [Route("AddBooking")]
        public IActionResult AddBooking(string data)
        {
            Booking booking = JsonConvert.DeserializeObject<Booking>(data);
            _bookingRepository.AddBooking(booking);

            return Ok();
        }

        [HttpDelete]
        [Route("deleteBooking")]
        public IActionResult DeleteBooking(string data)
        {
            Guid bookingId = JsonConvert.DeserializeObject<Guid>(data);
            _bookingRepository.DeleteBooking(bookingId);

            return Ok();
        }

        [HttpPut]
        [Route("updateBooking")]
        public IActionResult UpdateBooking(string data)
        {
            Booking booking = JsonConvert.DeserializeObject<Booking>(data);
            _bookingRepository.UpdateBooking(booking);

            return Ok();
        }
    }
}
