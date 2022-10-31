using System.Threading.Tasks;
using MRA.Bookings.Repositories;
using MRA.Bookings.Models;
using Newtonsoft.Json;
using System;
using Microsoft.Extensions.Configuration;
using MRA.Bookings.Logic.SignalR;

namespace MRA.Bookings.Logic.RabbitMQ.Consumers
{
    public class RabbitMqAddListener : RabbitMqBaseListener
    {
        public RabbitMqAddListener(IServiceProvider provider, IConfiguration configuration, ISignalRClient signalRClient)
            : base(provider, configuration, signalRClient) { }

        public override void Configure(IConfiguration configuration)
        {
            this.hostName = configuration.GetSection("RabbitMQ").GetValue<string>("HostName");
            this.exchangeName = configuration.GetSection("RabbitMQ").GetValue<string>("ExchangeName");
            this.exchangeType = configuration.GetSection("RabbitMQ").GetValue<string>("ExchangeType");
            this.queueName = configuration.GetSection("RabbitMQ").GetValue<string>("AddQueueName");
            this.routingKey = configuration.GetSection("RabbitMQ").GetValue<string>("AddRoutingKey");
        }

        public override async Task<(NotificationDto, string)> OperationAsync(IBookingRepository bookingRepository, string jsonData)
        {
            var bookingTokenDto = JsonConvert.DeserializeObject<BookingTokenDto>(jsonData);
            NotificationDto notification;

            if (await bookingRepository.AddBookingAsync(bookingTokenDto.Booking))
            {
                notification = new NotificationDto()
                {
                    Message = $"Your booking at {bookingTokenDto.Booking.StartTime.ToString()} has been successfully added!",
                    Successfully = true
                };
            }
            else
            {
                notification = new NotificationDto()
                {
                    Message = $"Your booking at {bookingTokenDto.Booking.StartTime.ToString()} has not been added!",
                    Successfully = false
                };
            }

            return (notification, bookingTokenDto.Token);
        }
    }
}
