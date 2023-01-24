using System;
using System.Collections.Generic;
using System.Linq;

namespace SignalR.API.Hubs
{
    public class ChatManager
    {
        public readonly List<ChatUser> Users = new List<ChatUser>();

        public void ConnectUser(string userName, string connectionId)
        {
            var userAlreadyExists = GetConnectedUserByName(userName);
            if (userAlreadyExists != null)
            {
                userAlreadyExists.AppendConnection(connectionId);
                return;
            }

            var user = new ChatUser(userName);
            user.AppendConnection(connectionId);
            Users.Add(user);
        }

        public bool DisconnectUser(string connectionId)
        {
            var userExists = GetUserByConnectionId(connectionId);
            if (userExists == null)
            {
                return false;
            }

            if (!userExists.Connections.Any())
            {
                return false;
            }

            var connectionExists = userExists.Connections.Where(x => x.ConnectionId == connectionId).Count() == 1;
            if (!connectionExists)
            {
                return false;
            }

            if (userExists.Connections.Count() == 1)
            {
                Users.Remove(userExists);
                return true;
            }

            userExists.RemoveConnection(connectionId);
            return false;
        }

        private ChatUser GetUserByConnectionId(string connectionId)
        {
            return Users
                .FirstOrDefault(x => x.Connections.Select(c => c.ConnectionId)
                .Contains(connectionId));
        }

        private ChatUser GetConnectedUserByName(string userName)
        {
            return Users
                .FirstOrDefault(x => string.Equals(
                    x.UserName,
                    userName,
                    StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
