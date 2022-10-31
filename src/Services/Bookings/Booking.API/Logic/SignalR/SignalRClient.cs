using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.SignalR.Client;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace MRA.Bookings.Logic.SignalR
{
    public class SignalRClient : ISignalRClient
    {
        //const string authUrl = "http://localhost:5000/connect/token";
        const string hubUrl = "http://localhost:5400/notification";

        //HubConnection connection;

        public SignalRClient()
        {

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

            connection.StartAsync().GetAwaiter().GetResult();

            try
            {
                await connection.SendAsync("SendNotificationAsync", message);
                connection.StopAsync().GetAwaiter().GetResult();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
