using System;

namespace MRA.Gateway.Models
{
    public class OrderEditDto
    {
        public Guid Id { get; set; }
        public Guid MeetingRoomId { get; set; }
        public string MeetingRoomName { get; set; }
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
