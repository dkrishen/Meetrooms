using System;
using System.Collections.Generic;
using System.Linq;

namespace SignalR.API.Hubs
{
    public class ChatUser
    {
        public List<ChatConnection> Connections { get; }
        public string UserName { get; set; }

        public ChatUser(string UserName)
        {
            Connections = new List<ChatConnection>();
            this.UserName = UserName;
        }

        public void AppendConnection(string connectionId)
        {
            if (connectionId == null)
            {
                throw new ArgumentNullException(nameof(connectionId));
            }

            var connection = new ChatConnection
            {
                ConnectedAt = DateTime.UtcNow,
                ConnectionId = connectionId
            };

            Connections.Add(connection);
        }

        public void RemoveConnection(string connectionId)
        {
            if (connectionId == null)
            {
                throw new ArgumentNullException(nameof(connectionId));
            }

            var connection = Connections.SingleOrDefault(x => x.ConnectionId.Equals(connectionId));

            if(connection == null)
            {
                return;
            }

            Connections.Remove(connection);
        }

        public DateTime? ConnectedAt
        {
            get
            {
                if (Connections.Any())
                {
                    return Connections
                        .OrderByDescending(x => x.ConnectedAt)
                        .Select(x => x.ConnectedAt)
                        .First();
                }

                return null;
            }
        }

    }
}
