﻿using Microsoft.AspNetCore.Connections;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Diagnostics.Metrics;
using System.Text;

namespace Gateway.API.Logic.RabbitMQ
{
    public class RabbitProducer
    {
        private string hostName = "localhost";
        private string exchangeName = "mra-gateway-ex";
        private string exchangeType = ExchangeType.Topic;

        public bool Publish(string serviceName, string operationMethod, object data)
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = hostName };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: exchangeName, type: exchangeType);

                    string routingKey = $"{serviceName}.{operationMethod}";

                    string jsonData = JsonConvert.SerializeObject(data);
                    var body = Encoding.UTF8.GetBytes(jsonData);

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