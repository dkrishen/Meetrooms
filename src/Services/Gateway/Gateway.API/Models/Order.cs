using System;

namespace MRA.Gateway.Models
{
    public class Order
    {
        public Guid MeetingRoomId { get; set; }
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
