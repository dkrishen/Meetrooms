using MRA.Bookings.Models;
using MRA.Bookings.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using MRA.Bookings.Logic;

namespace MRA.Bookings.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly ILogger<BookingController> _logger;
        private readonly IBookingLogic _bookingLogic;

        public BookingController(ILogger<BookingController> logger, IBookingLogic bookingLogic)
        {
            _logger = logger;
            _bookingLogic = bookingLogic;
        }

        [HttpGet]
        [Route("AllBookings")]
        public async Task<IActionResult> GetBookingsAsync()
        {
            var bookings = await _bookingLogic.GetBookingsAsync();
            return Ok(bookings);
        }

        [HttpGet]
        [Route("BookingsByUserId")]
        public async Task<IActionResult> GetBookingsByUserIdAsync(string data)
        {
            Guid userId = JsonConvert.DeserializeObject<Guid>(data);
            return Ok(await _bookingLogic.GetBookingsByUserIdAsync(userId));
        }
    }
}
