using MRA.Gateway.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gateway.API.Logic
{
    public interface IBookingLogic
    {
        public Task<IEnumerable<BookingViewModel>> GetBookingsAsync(string token);
        public Task<IEnumerable<BookingViewModel>> GetBookingsByUserIdAsync(Guid id, string token);
        public bool AddBooking(BookingInputDto booking, Guid id, string token);
        public bool UpdateBooking(BookingEditDto booking, string token);
        public bool DeleteBooking(Guid id, string token);
    }
}
