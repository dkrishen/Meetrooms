using System;

namespace MRA.Bookings.Models
{
    public partial class Booking
    {
        public Guid Id { get; set; }
        public Guid MeetingRoomId { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
