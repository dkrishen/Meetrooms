using MRA.Gateway.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Gateway.API.Logic;

namespace MRA.Gateway.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : Controller
    {
        IBookingLogic _bookingLogic;

        public BookingController(
            IBookingLogic bookingLogic)
        {
            _bookingLogic = bookingLogic;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetAllBookingsAsync()
        {
            var authorizationHeaderValue = Request.Headers["Authorization"].ToString();
            var result = (await _bookingLogic.GetBookingsAsync(
                authorizationHeaderValue)).ToList();

            return Ok(result);
        }

        [HttpGet]
        [Route("My")]
        public async Task<IActionResult> GetBookingsByUserAsync()
        {
            var authorizationHeaderValue = Request.Headers["Authorization"].ToString();
            var userId = Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
            
            var result = (await _bookingLogic.GetBookingsByUserIdAsync(
                userId, authorizationHeaderValue)).ToList();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddBookingAsync([FromBody] BookingInputDto booking)
        {
            var authorizationHeaderValue = Request.Headers["Authorization"].ToString();
            var userId = Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
            
            bool result = _bookingLogic
                .AddBooking(booking, userId, authorizationHeaderValue);

            if (result)
            {
                return StatusCode(202);
            }
            else
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        public IActionResult DeleteBookingAsync([FromBody] GuidDto data)
        {
            var authorizationHeaderValue = Request.Headers["Authorization"].ToString();
            bool result = _bookingLogic
                .DeleteBooking(data.id, authorizationHeaderValue);

            if (result)
            {
                return StatusCode(202);
            }
            else
            {
                return StatusCode(500);
            }
        }

        [HttpPut]
        public IActionResult UpdateBookingAsync([FromBody] BookingEditDto booking)
        {
            var authorizationHeaderValue = Request.Headers["Authorization"].ToString();
            bool result = _bookingLogic
                .UpdateBooking(booking, authorizationHeaderValue);

            if (result)
            {
                return StatusCode(202);
            }
            else
            {
                return StatusCode(500);
            }
        }
    }
}
