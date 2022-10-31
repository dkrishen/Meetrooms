using System;

namespace SignalR.API.Hubs
{
    public class ChatConnection
    {
        public DateTime ConnectedAt { get; set; }
        public string ConnectionId { get; set; } = null!;
    }
}
