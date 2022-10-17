using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
 using System.Linq;
using System.Threading.Tasks;

namespace SignalR.API.Hubs
{
    [Authorize]
    public class NotificationHub : Hub<INotificationHub>
    {
        private ChatManager chatManager;
        private const string defaultGroupName = "General";

        public NotificationHub(ChatManager chatManager)
        {
            this.chatManager = chatManager;
        }

        public override async Task OnConnectedAsync()
        {
            var userName = Context.User?.Identity?.Name ?? "Anonymous";
            var connectionId = Context.ConnectionId;
            chatManager.ConnectUser(userName, connectionId);

            await Groups.AddToGroupAsync(connectionId, defaultGroupName);
            await UpdateUsersAsync();
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var isUserRemoved = chatManager.DisconnectUser(Context.ConnectionId);
            if (!isUserRemoved)
            {
                await base.OnDisconnectedAsync(exception);
            }

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, defaultGroupName);
            await UpdateUsersAsync();
            await base.OnDisconnectedAsync(exception);
        }

        public async Task UpdateUsersAsync()
        {
            var users = chatManager.Users.Select(x => x.UserName + " " + x.Connections.Count.ToString()).ToList();
            await Clients.All.UpdateUsersAsync(users);
        }

        public async Task SendNotificationAsync(string message)
        {
            var userName = Context.User?.Identity?.Name ?? "Anonymous";
            await Clients
                .Clients(chatManager
                    .Users
                    .Where(x => x.UserName == userName)
                    .FirstOrDefault()
                    .Connections
                    .Select(x => x.ConnectionId))
                .SendNotificationAsync(message);
        }
    }
}
