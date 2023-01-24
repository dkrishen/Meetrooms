using AutoMapper;
using MRA.Gateway.Models;
using MRA.Gateway.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.API.Logic
{
    public class BookingLogic : IBookingLogic
    {
        IMapper _mapper;
        IRoomRepository _roomRepository;
        IUserRepository _userRepository;
        IBookingRepository _bookingRepository;
        
        public BookingLogic(
            IMapper mapper,
            IRoomRepository roomRepository,
            IUserRepository userRepository,
            IBookingRepository bookingRepository)
        {
            _mapper = mapper;
            _roomRepository = roomRepository;
            _userRepository = userRepository;
            _bookingRepository = bookingRepository;
        }

        public bool AddBooking(BookingInputDto booking, Guid id, string token)
        {
            var newBooking = _mapper.Map<Booking>(booking);
            // temporary GUID the single meeting room (have not ability for select rooms at the moment)
            newBooking.MeetingRoomId = new Guid("1DDA7260-08E8-4B32-A9EE-F7E1CA69BC9C");
            newBooking.UserId = id;


            return _bookingRepository
                .AddBooking(newBooking, token);
        }

        private IEnumerable<BookingViewModel> ComposeBookingViewModels(
            IEnumerable<Booking> bookings,
            IEnumerable<UserShortDto> users,
            IEnumerable<MeetingRoom> rooms)
        {
            var result = new List<BookingViewModel>();
            foreach (var booking in bookings)
            {
                var bookingViewModel = _mapper.Map<BookingViewModel>(booking);
                bookingViewModel.Username = users?
                    .Where(u => u.Id == booking.UserId)?
                    .FirstOrDefault()?.Username ?? "[DELETED USER]";
                bookingViewModel.MeetingRoomName = rooms?
                    .Where(r => r.Id == booking.MeetingRoomId)?
                    .FirstOrDefault()?.Name ?? "[DELETED ROOM]";
                result.Add(bookingViewModel);
            }

            return result;
        }

        private IEnumerable<BookingViewModel> ComposeBookingViewModels(
            IEnumerable<Booking> bookings,
            UserShortDto user,
            IEnumerable<MeetingRoom> rooms)
        {
            var result = new List<BookingViewModel>();
            foreach (var booking in bookings)
            {
                var bookingViewModel = _mapper.Map<BookingViewModel>(booking);
                bookingViewModel.Username = user?.Username ?? "[ERROR]";
                bookingViewModel.MeetingRoomName = rooms?
                    .Where(r => r.Id == booking.MeetingRoomId)?
                    .FirstOrDefault()?.Name ?? "[DELETED ROOM]";
                result.Add(bookingViewModel);
            }

            return result;
        }

        public bool DeleteBooking(Guid id, string token)
        {
            return _bookingRepository
                .DeleteBooking(id, token);
        }

        public async Task<IEnumerable<BookingViewModel>> GetBookingsAsync(string token)
        {
            var bookings = (await _bookingRepository
                .GetBookingsAsync(token))
                .ToList();

            var roomIds = bookings
                .Select(o => o.MeetingRoomId)
                .ToHashSet<Guid>();

            var userIds = bookings
                .Select(o => o.UserId)
                .ToHashSet<Guid>();

            var roomsRequest = _roomRepository
                .GetRoomsByRoomIdsAsync(roomIds, token);
            var usersRequest = _userRepository
                .GetUsersByIdsAsync(userIds, token);

            Task.WaitAll(roomsRequest, usersRequest);

            var rooms = roomsRequest.Result.ToList();
            var users = usersRequest.Result.ToList();

            return ComposeBookingViewModels(
                bookings, users, rooms).ToList();
        }

        public async Task<IEnumerable<BookingViewModel>> GetBookingsByUserIdAsync(Guid id, string token)
        {
            var bookings = (await _bookingRepository
                .GetBookingsByUserAsync(id, token))
                .ToList();

            var roomIds = bookings
                .Select(o => o.MeetingRoomId)
                .ToHashSet<Guid>();

            var roomsRequest = _roomRepository
                .GetRoomsByRoomIdsAsync(roomIds, token);

            var userRequest = _userRepository
                .GetUsersByIdsAsync(new HashSet<Guid>() { id }, token);

            Task.WaitAll(roomsRequest, userRequest);

            var rooms = roomsRequest.Result.ToList();
            var user = userRequest.Result.FirstOrDefault();

           return ComposeBookingViewModels(
                bookings, user, rooms).ToList();
        }

        public bool UpdateBooking(BookingEditDto booking, string token)
        {
            return _bookingRepository
                .UpdateBooking(_mapper.Map<Booking>(booking), token);
        }
    }
}
