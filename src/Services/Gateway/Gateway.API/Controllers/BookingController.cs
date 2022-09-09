using AutoMapper;
using MRA.Gateway.Models;
using MRA.Gateway.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Diagnostics;
using Microsoft.AspNetCore.Hosting.Infrastructure;

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
        public IActionResult GetAllBookings()
        {
            var authorizationHeaderValue = Request.Headers["Authorization"].ToString();
            var bookings = _bookingRepository.GetBookings(authorizationHeaderValue).ToList();
            
            var roomIds = bookings.Select(o => o.MeetingRoomId).ToHashSet<Guid>();
            var rooms = _meetingRoomRepository.GetRoomsByRoomIds(roomIds, authorizationHeaderValue);

            var userIds = bookings.Select(o => o.UserId).ToHashSet<Guid>();
            var users = _userRepository.GetUsersByIds(userIds, authorizationHeaderValue).ToList();

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
        public IActionResult GetBookingsByUser()
        {
            var authorizationHeaderValue = Request.Headers["Authorization"].ToString();
            var userId = Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var bookings = _bookingRepository.GetBookingsByUser(userId, authorizationHeaderValue);

            var roomIds = bookings.Select(o => o.MeetingRoomId).ToHashSet<Guid>();
            var rooms = _meetingRoomRepository.GetRoomsByRoomIds(roomIds, authorizationHeaderValue);

            var user = _userRepository.GetUsersByIds(new HashSet<Guid>() { userId }, authorizationHeaderValue).FirstOrDefault();

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
        public IActionResult AddBooking([FromBody] BookingInputDto booking)
        {
            var authorizationHeaderValue = Request.Headers["Authorization"].ToString();
            var userId = Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
            
            var newBooking = _mapper.Map<Booking>(booking);
            newBooking.MeetingRoomId = new Guid("1DDA7260-08E8-4B32-A9EE-F7E1CA69BC9C");  // temporary GUID the single meeting room (have not ability for select rooms at the moment)
            newBooking.UserId = userId;

            _bookingRepository.AddBooking(newBooking, authorizationHeaderValue);
            return Ok();
        }

        [HttpDelete]
        [Route("DeleteBooking")]
        public IActionResult DeleteBooking([FromBody] GuidDto data)
        {
            var authorizationHeaderValue = Request.Headers["Authorization"].ToString();
            _bookingRepository.DeleteBooking(data.id, authorizationHeaderValue);
            return Ok();
        }

        [HttpPut]
        [Route("UpdateBooking")]
        public IActionResult UpdateBooking([FromBody] BookingEditDto booking)
        {
            var authorizationHeaderValue = Request.Headers["Authorization"].ToString();
            _bookingRepository.UpdateBooking(_mapper.Map<Booking>(booking), authorizationHeaderValue);
            return Ok();
        }
    }
}
