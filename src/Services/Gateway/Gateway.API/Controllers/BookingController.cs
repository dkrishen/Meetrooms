using AutoMapper;
using MRA.Gateway.Models;
using MRA.Gateway.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MRA.Gateway.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : Controller
    {
        IMeetingRoomRepository _meetingRoomRepository;
        IUserRepository _userRepository;
        IBookingRepository _bookingRepository;
        IMapper _mapper;

        public BookingController(
            IMeetingRoomRepository meetingRoomRepository, 
            IUserRepository userRepository, 
            IBookingRepository bookingRepository,
            IMapper mapper)
        {
            _meetingRoomRepository = meetingRoomRepository;
            _userRepository = userRepository;
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllBookings")]
        public async Task<IActionResult> GetAllBookingsAsync()
        {
            var authorizationHeaderValue = Request.Headers["Authorization"].ToString();
            var bookings = (await _bookingRepository.GetBookingsAsync(authorizationHeaderValue)).ToList();
            
            var roomIds = bookings.Select(o => o.MeetingRoomId).ToHashSet<Guid>();
            var rooms = (await _meetingRoomRepository.GetRoomsByRoomIdsAsync(roomIds, authorizationHeaderValue)).ToList();

            var userIds = bookings.Select(o => o.UserId).ToHashSet<Guid>();
            var users = (await _userRepository.GetUsersByIdsAsync(userIds, authorizationHeaderValue)).ToList();

            var result = new List<BookingViewModel>();
            foreach (var booking in bookings)
            {
                var bookingViewModel = _mapper.Map<BookingViewModel>(booking);
                bookingViewModel.Username = users?.Where(u => u.Id == booking.UserId)?.SingleOrDefault()?.Username ?? "[DELETED USER]";
                bookingViewModel.MeetingRoomName = rooms?.Where(r => r.Id == booking.MeetingRoomId)?.FirstOrDefault()?.Name ?? "[DELETED ROOM]";
                result.Add(bookingViewModel);
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("GetBookingsByUser")]
        public async Task<IActionResult> GetBookingsByUserAsync()
        {
            var authorizationHeaderValue = Request.Headers["Authorization"].ToString();
            var userId = Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var bookings = (await _bookingRepository.GetBookingsByUserAsync(userId, authorizationHeaderValue)).ToList();

            var roomIds = bookings.Select(o => o.MeetingRoomId).ToHashSet<Guid>();
            var rooms = (await _meetingRoomRepository.GetRoomsByRoomIdsAsync(roomIds, authorizationHeaderValue)).ToList();

            var user = (await _userRepository.GetUsersByIdsAsync(new HashSet<Guid>() { userId }, authorizationHeaderValue)).FirstOrDefault();

            var result = new List<BookingViewModel>();
            foreach (var booking in bookings)
            {
                var bookingViewModel = _mapper.Map<BookingViewModel>(booking);
                bookingViewModel.Username = user?.Username ?? "[ERROR]";
                bookingViewModel.MeetingRoomName = rooms?.Where(r => r.Id == booking.MeetingRoomId)?.FirstOrDefault()?.Name ?? "[DELETED ROOM]";
                result.Add(bookingViewModel);
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("AddBooking")]
        public async Task<IActionResult> AddBookingAsync([FromBody] BookingInputDto booking)
        {
            var authorizationHeaderValue = Request.Headers["Authorization"].ToString();
            var userId = Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
            
            var newBooking = _mapper.Map<Booking>(booking);
            // temporary GUID the single meeting room (have not ability for select rooms at the moment)
            newBooking.MeetingRoomId = new Guid("1DDA7260-08E8-4B32-A9EE-F7E1CA69BC9C");  
            newBooking.UserId = userId;

            bool result = await _bookingRepository.AddBookingAsync(newBooking, authorizationHeaderValue);
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteBooking")]
        public async Task<IActionResult> DeleteBookingAsync([FromBody] GuidDto data)
        {
            var authorizationHeaderValue = Request.Headers["Authorization"].ToString();
            bool result = await _bookingRepository.DeleteBookingAsync(data.id, authorizationHeaderValue);
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateBooking")]
        public async Task<IActionResult> UpdateBookingAsync([FromBody] BookingEditDto booking)
        {
            var authorizationHeaderValue = Request.Headers["Authorization"].ToString();
            bool result = await _bookingRepository.UpdateBookingAsync(_mapper.Map<Booking>(booking), authorizationHeaderValue);
            return Ok(result);
        }
    }
}
