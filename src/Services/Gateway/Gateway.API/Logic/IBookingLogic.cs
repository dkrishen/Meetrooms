using MRA.Gateway.Models;
using System.Collections;
using System.Collections.Generic;

namespace Gateway.API.Logic
{
    public interface IBookingLogic
    {
        public IEnumerable<BookingViewModel> ComposeBookingViewModels(
            IEnumerable<Booking> bookings,
            IEnumerable<UserShortDto> users,
            IEnumerable<MeetingRoom> rooms);

        public IEnumerable<BookingViewModel> ComposeBookingViewModels(
            IEnumerable<Booking> bookings,
            UserShortDto user,
            IEnumerable<MeetingRoom> rooms);
    }
}
