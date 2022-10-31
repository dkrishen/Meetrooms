using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace Gateway.API.Logic.RabbitMQ
{
    public class RabbitProducer
    {
        private string hostName;
        private int port;
        private string userName;
        private string password;
        private string exchangeName;
        private string exchangeType;

        public RabbitProducer(IConfiguration configuration)
        {
            this.hostName = configuration.GetSection("RabbitMQ").GetValue<string>("HostName");
            this.port = configuration.GetSection("RabbitMQ").GetValue<int>("Port");
            this.userName = configuration.GetSection("RabbitMQ").GetValue<string>("UserName");
            this.password = configuration.GetSection("RabbitMQ").GetValue<string>("Password");
            this.exchangeName = configuration.GetSection("RabbitMQ").GetValue<string>("ExchangeName");
            this.exchangeType = configuration.GetSection("RabbitMQ").GetValue<string>("ExchangeType");
        }

        public bool Publish(string serviceName, string operationMethod, object data)
        {
            try
            {
                var factory = new ConnectionFactory() { 
                    HostName = hostName, 
                    Port = port, 
                    UserName = userName, 
                    Password = password
                };

                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: exchangeName, type: exchangeType);

                    string routingKey = $"{serviceName}.{operationMethod}";

                    string jsonData = JsonConvert.SerializeObject(data);
                    var body = Encoding.UTF8.GetBytes(jsonData);

                    Console.WriteLine("LOG: " + routingKey + '\n' + jsonData);

                    channel.BasicPublish(exchange: exchangeName,
                        routingKey: routingKey,
                        basicProperties: null,
                        body: body);
                };
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
