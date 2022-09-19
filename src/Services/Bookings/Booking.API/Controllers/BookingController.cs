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
            return Ok(bookings);
        }

        [HttpGet]
        [Route("GetBookingsByUserId")]
        public IActionResult GetBookingsByUserId(string data)
        {
            Guid userId = JsonConvert.DeserializeObject<Guid>(data);
            return Ok(_bookingRepository.GetBookingsByUser(userId));
        }

        [HttpPost]
        [Route("AddBooking")]
        public IActionResult AddBooking([FromBody] Booking data)
        {
            _bookingRepository.AddBooking(data);

            return Ok(true);
        }

        [HttpDelete]
        [Route("deleteBooking")]
        public IActionResult DeleteBooking([FromBody] Guid data)
        {
           _bookingRepository.DeleteBooking(data);

            return Ok(true);
        }

        [HttpPut]
        [Route("updateBooking")]
        public IActionResult UpdateBooking([FromBody] Booking data)
        {
            _bookingRepository.UpdateBooking(data);

            return Ok(true);
        }
    }
}
