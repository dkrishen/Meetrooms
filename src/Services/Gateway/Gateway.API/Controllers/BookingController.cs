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
using Gateway.API.Logic;

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
        IBookingLogic _bookingLogic;

        public BookingController(
            IMeetingRoomRepository meetingRoomRepository, 
            IUserRepository userRepository, 
            IBookingRepository bookingRepository,
            IMapper mapper,
            IBookingLogic bookingLogic)
        {
            _meetingRoomRepository = meetingRoomRepository;
            _userRepository = userRepository;
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _bookingLogic = bookingLogic;
        }

        [HttpGet]
        [Route("GetAllBookings")]
        public async Task<IActionResult> GetAllBookingsAsync()
        {
            var authorizationHeaderValue = Request.Headers["Authorization"].ToString();
            var bookings = (await _bookingRepository
                .GetBookingsAsync(authorizationHeaderValue))
                .ToList();
            
            var roomIds = bookings
                .Select(o => o.MeetingRoomId)
                .ToHashSet<Guid>();

            var userIds = bookings
                .Select(o => o.UserId)
                .ToHashSet<Guid>();

            var roomsRequest = _meetingRoomRepository
                .GetRoomsByRoomIdsAsync(roomIds, authorizationHeaderValue);
            var usersRequest = _userRepository
                .GetUsersByIdsAsync(userIds, authorizationHeaderValue);

            Task.WaitAll();

            var rooms = roomsRequest.Result.ToList();
            var users = usersRequest.Result.ToList();

            var result = _bookingLogic.ComposeBookingViewModels(
                bookings, users, rooms).ToList();

            return Ok(result);
        }

        [HttpGet]
        [Route("GetBookingsByUser")]
        public async Task<IActionResult> GetBookingsByUserAsync()
        {
            var authorizationHeaderValue = Request.Headers["Authorization"].ToString();
            var userId = Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
            
            var bookings = (await _bookingRepository
                .GetBookingsByUserAsync(userId, authorizationHeaderValue))
                .ToList();

            var roomIds = bookings
                .Select(o => o.MeetingRoomId)
                .ToHashSet<Guid>();

            var roomsRequest = _meetingRoomRepository
                .GetRoomsByRoomIdsAsync(roomIds, authorizationHeaderValue);

            var userRequest = _userRepository
                .GetUsersByIdsAsync(new HashSet<Guid>() { userId }, authorizationHeaderValue);

            Task.WaitAll();

            var rooms = roomsRequest.Result.ToList();
            var user = userRequest.Result.FirstOrDefault();

            var result = _bookingLogic.ComposeBookingViewModels(
                bookings, user, rooms).ToList();

            return Ok(result);
        }

        [HttpPost]
        [Route("AddBooking")]
        public IActionResult AddBookingAsync([FromBody] BookingInputDto booking)
        {
            var authorizationHeaderValue = Request.Headers["Authorization"].ToString();
            var userId = Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
            
            var newBooking = _mapper.Map<Booking>(booking);
            // temporary GUID the single meeting room (have not ability for select rooms at the moment)
            newBooking.MeetingRoomId = new Guid("1DDA7260-08E8-4B32-A9EE-F7E1CA69BC9C");  
            newBooking.UserId = userId;

            bool result = _bookingRepository
                .AddBooking(newBooking, authorizationHeaderValue);
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteBooking")]
        public IActionResult DeleteBookingAsync([FromBody] GuidDto data)
        {
            var authorizationHeaderValue = Request.Headers["Authorization"].ToString();
            bool result = _bookingRepository
                .DeleteBooking(data.id, authorizationHeaderValue);
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateBooking")]
        public IActionResult UpdateBookingAsync([FromBody] BookingEditDto booking)
        {
            var authorizationHeaderValue = Request.Headers["Authorization"].ToString();
            bool result = _bookingRepository
                .UpdateBooking(_mapper.Map<Booking>(booking), authorizationHeaderValue);
            return Ok(result);
        }
    }
}
