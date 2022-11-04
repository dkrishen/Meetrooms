using System;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace MRA.Bookings.Logic.SignalR
{
    public class SignalRClient : ISignalRClient
    {
        string hubUrl;

        public SignalRClient(IConfiguration configuration)
        {
            hubUrl = configuration.GetSection("SignalR").GetValue<string>("NotificationHubURL");
        }

        public async Task SendNotificationAsync(string message, string token)
        {
            Console.WriteLine("Creating connection...");
            var connection = new HubConnectionBuilder()
                .WithUrl(hubUrl, options =>
                {
                    options.Headers.Add("Authorization", token);
                })
                .WithAutomaticReconnect()
                .Build();
            try
            {
                connection.StartAsync().GetAwaiter().GetResult();
                await connection.SendAsync("SendNotificationAsync", message);
                await connection.SendAsync("UpdateCalendarAsync");
                connection.StopAsync().GetAwaiter().GetResult();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
