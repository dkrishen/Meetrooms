using AutoMapper;
using MRA.Gateway.Models;
using System.Collections.Generic;
using System.Linq;

namespace Gateway.API.Logic
{
    public class BookingLogic : IBookingLogic
    {
        IMapper _mapper;

        public BookingLogic(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<BookingViewModel> ComposeBookingViewModels(
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
                    .SingleOrDefault()?.Username ?? "[DELETED USER]";
                bookingViewModel.MeetingRoomName = rooms?
                    .Where(r => r.Id == booking.MeetingRoomId)?
                    .FirstOrDefault()?.Name ?? "[DELETED ROOM]";
                result.Add(bookingViewModel);
            }

            return result;
        }

        public IEnumerable<BookingViewModel> ComposeBookingViewModels(
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
    }
}
