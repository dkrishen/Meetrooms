using System.Threading.Tasks;
using MRA.Bookings.Repositories;
using MRA.Bookings.Models;
using Newtonsoft.Json;
using System;
using Microsoft.Extensions.Configuration;
using MRA.Bookings.Logic.SignalR;

namespace MRA.Bookings.Logic.RabbitMQ.Consumers
{
    public class RabbitMqUpdateListener : RabbitMqBaseListener
    {
        public RabbitMqUpdateListener(IServiceProvider provider, IConfiguration configuration, ISignalRClient signalRClient)
            : base(provider, configuration, signalRClient) { }

        public override void Configure(IConfiguration configuration)
        {
            this.hostName = configuration.GetSection("RabbitMQ").GetValue<string>("HostName");
            this.exchangeName = configuration.GetSection("RabbitMQ").GetValue<string>("ExchangeName");
            this.exchangeType = configuration.GetSection("RabbitMQ").GetValue<string>("ExchangeType");
            this.queueName = configuration.GetSection("RabbitMQ").GetValue<string>("UpdateQueueName");
            this.routingKey = configuration.GetSection("RabbitMQ").GetValue<string>("UpdateRoutingKey");
        }

        public override async Task<(NotificationDto, string)> OperationAsync(IBookingLogic bookingLogic, string jsonData)
        {
            var bookingTokenDto = JsonConvert.DeserializeObject<BookingTokenDto>(jsonData);
            NotificationDto notification;

            if(await bookingLogic.UpdateBookingAsync(bookingTokenDto.Booking))
            {
                notification = new NotificationDto()
                {
                    Message = $"Your booking at {bookingTokenDto.Booking.StartTime.ToString()} has been successfully edited!",
                    Successfully = true
                };
            }
            else
            {
                notification = new NotificationDto()
                {
                    Message = $"Your booking at {bookingTokenDto.Booking.StartTime.ToString()} has not been edited!",
                    Successfully = false
                };
            }

            return (notification, bookingTokenDto.Token);
        }
    }
}
