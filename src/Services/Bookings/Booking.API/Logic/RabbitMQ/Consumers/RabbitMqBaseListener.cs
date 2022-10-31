using Microsoft.Extensions.Configuration;
using MRA.Bookings.Logic.SignalR;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Threading.Tasks;
using System.Threading;
using System;
using Microsoft.Extensions.Hosting;
using System.Text;
using Newtonsoft.Json;
using MRA.Bookings.Models;
using Microsoft.Extensions.DependencyInjection;
using MRA.Bookings.Repositories;

namespace MRA.Bookings.Logic.RabbitMQ.Consumers
{
    public abstract class RabbitMqBaseListener : BackgroundService
    {
        protected string hostName;
        protected string exchangeName;
        protected string exchangeType;
        protected string queueName;
        protected string routingKey;
        protected IConnection _connection;
        protected IModel _channel;
        protected IServiceProvider _serviceProvider;
        protected readonly ISignalRClient signalRClient;

        public RabbitMqBaseListener(IServiceProvider provider, IConfiguration configuration, ISignalRClient signalRClient)
        {
            Configure(configuration);
            _serviceProvider = provider;
            this.signalRClient = signalRClient;
            var factory = new ConnectionFactory { HostName = hostName };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: exchangeName, type: exchangeType);

            _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: routingKey);
        }

        public abstract void Configure(IConfiguration configuration);

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (stoppingToken.IsCancellationRequested)
            {
                _channel.Dispose();
                _connection.Dispose();
                return Task.CompletedTask;
            }

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (ch, ea) =>
            {
                var jsonData = Encoding.UTF8.GetString(ea.Body.ToArray());

                using (var scope = _serviceProvider.CreateScope())
                {
                    var bookingRepository = scope.ServiceProvider.GetRequiredService<IBookingRepository>();

                    (var notification, string token) = await OperationAsync(bookingRepository, jsonData);

                    var notificationJson = JsonConvert.SerializeObject(notification);

                    await signalRClient.SendNotificationAsync(notificationJson, token);
                }

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(queueName, false, consumer);
            return Task.CompletedTask;
        }

        public abstract Task<(NotificationDto, string)> OperationAsync(IBookingRepository bookingRepository, string jsonData);

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
