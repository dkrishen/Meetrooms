using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalR.API.Hubs
{
    public interface INotificationHub
    {
        Task SendNotificationAsync(string notification);
        Task UpdateUsersAsync(IEnumerable<string> users);
    }
}