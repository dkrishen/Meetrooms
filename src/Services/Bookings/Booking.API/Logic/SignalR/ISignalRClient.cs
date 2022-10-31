using System.Threading.Tasks;

namespace MRA.Bookings.Logic.SignalR
{
    public interface ISignalRClient
    {
        public Task SendNotificationAsync(string message, string token);
    }
}