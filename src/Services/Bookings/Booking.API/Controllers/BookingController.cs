using MRA.Bookings.Models;
using MRA.Bookings.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetBookingsAsync()
        {
            var bookings = await _bookingRepository.GetBookingsAsync();
            return Ok(bookings);
        }

        [HttpGet]
        [Route("GetBookingsByUserId")]
        public async Task<IActionResult> GetBookingsByUserIdAsync(string data)
        {
            Guid userId = JsonConvert.DeserializeObject<Guid>(data);
            return Ok(await _bookingRepository.GetBookingsByUserAsync(userId));
        }

        [HttpPost]
        [Route("AddBooking")]
        public async Task<IActionResult> AddBookingAsync([FromBody] Booking data)
        {
            await _bookingRepository.AddBookingAsync(data);

            return Ok(true);
        }

        [HttpDelete]
        [Route("deleteBooking")]
        public async Task<IActionResult> DeleteBookingAsync([FromBody] Guid data)
        {
           await _bookingRepository.DeleteBookingAsync(data);

            return Ok(true);
        }

        [HttpPut]
        [Route("updateBooking")]
        public async Task<IActionResult> UpdateBookingAsync([FromBody] Booking data)
        {
            await _bookingRepository.UpdateBookingAsync(data);

            return Ok(true);
        }
    }
}
