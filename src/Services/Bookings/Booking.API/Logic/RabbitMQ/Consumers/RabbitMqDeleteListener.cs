﻿using System.Threading.Tasks;
using MRA.Bookings.Repositories;
using Newtonsoft.Json;
using System;
using Microsoft.Extensions.Configuration;
using MRA.Bookings.Logic.SignalR;
using MRA.Bookings.Models;

namespace MRA.Bookings.Logic.RabbitMQ.Consumers
{
    public class RabbitMqDeleteListener : RabbitMqBaseListener
    {
        public RabbitMqDeleteListener(IServiceProvider provider, IConfiguration configuration, ISignalRClient signalRClient)
            : base(provider, configuration, signalRClient) { }

        public override void Configure(IConfiguration configuration)
        {
            this.hostName = configuration.GetSection("RabbitMQ").GetValue<string>("HostName");
            this.exchangeName = configuration.GetSection("RabbitMQ").GetValue<string>("ExchangeName");
            this.exchangeType = configuration.GetSection("RabbitMQ").GetValue<string>("ExchangeType");
            this.queueName = configuration.GetSection("RabbitMQ").GetValue<string>("DeleteQueueName");
            this.routingKey = configuration.GetSection("RabbitMQ").GetValue<string>("DeleteRoutingKey");
        }

        public override async Task<(NotificationDto, string)> OperationAsync(IBookingRepository bookingRepository, string jsonData)
        {
            var idTokenDto = JsonConvert.DeserializeObject<GuidTokenDto>(jsonData);
            NotificationDto notification;

            var booking = await bookingRepository.GetBookingByIdAsync(idTokenDto.Id);
            if (await bookingRepository.DeleteBookingAsync(idTokenDto.Id))
            {
                notification = new NotificationDto()
                {
                    Message = $"Your booking at {booking.StartTime.ToString()} has been successfully removed!",
                    Successfully = true
                };
            } 
            else
            {
                notification = new NotificationDto()
                {
                    Message = $"Your booking at {booking.StartTime.ToString()} has not been removed!",
                    Successfully = false
                };
            }

            return (notification, idTokenDto.Token);
        }
    }
}
