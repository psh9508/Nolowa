using Microsoft.AspNetCore.Connections;
using NolowaFrontend.Core.MessageQueue.Messages;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Text.Json;
using Ninject.Planning.Targets;

namespace NolowaFrontend.Core.MessageQueue
{
    public struct MessageQueueConnectionData
    {
        public string HostName { get; set; }
        public string VirtualHostName { get; set; }
        public string ExchangeName { get; set; }
        public string QueueName { get; set; }
        //public string RoutingKey { get; set; } = string.Empty; => "*.QueueName.*"

        public MessageQueueConnectionData(string hostName, string virtualHostName, string exchangeName, string queueName) 
        { 
            HostName = hostName;
            VirtualHostName = virtualHostName;
            ExchangeName = exchangeName;
            QueueName = queueName;
        }
    }

    public interface IMessageQueueService
    {
        Task<bool> InitAsync(MessageQueueConnectionData data);
        Task<bool> SendMessageAsync<T>(string target, T body) where T : MessageBase;
        void SendMessage<T>(T body) where T : MessageBase;
    }

    public class MessageQueueService : IMessageQueueService
    {
        private IModel _channel;
        private bool _isConnected;

        public MessageQueueService()
        {

        }

        public async Task<bool> InitAsync(MessageQueueConnectionData data)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var connectionFactory = new ConnectionFactory()
                    {
                        HostName = data.HostName,
                        VirtualHost = data.VirtualHostName,
                        UserName = "admin",
                        Password = "asdf1234",
                        SocketReadTimeout = TimeSpan.FromSeconds(10),
                        SocketWriteTimeout = TimeSpan.FromSeconds(10),
                    };

                    var connection = connectionFactory.CreateConnection();
                    _isConnected = true;

                    _channel = connection.CreateModel();
                    _channel.QueueDeclare(queue: data.QueueName, durable: true, exclusive: false);
                    _channel.QueueBind(queue: data.QueueName, exchange: data.ExchangeName, routingKey: $"*.{data.QueueName}.*");

                    var consumer = new EventingBasicConsumer(_channel);
                    consumer.Received += Consumer_Received;

                    _channel.BasicConsume(queue: data.QueueName, autoAck: true, consumer: consumer);

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            });
        }

        private void Consumer_Received(object? sender, BasicDeliverEventArgs e)
        {
            var body = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine("Received message: {0}", message);
        }

        public async Task<bool> SendMessageAsync<T>(string target, T body) where T : MessageBase
        {
            if (body is null || target  is null || _isConnected == false)
                return false;

            _channel.BasicPublish("amq.topic", $"h.{ target }.g", body: Encoding.UTF8.GetBytes(body.ToString()));

            return true;
        }


        public void SendMessage<T>(T body) where T : MessageBase
        {
            //if (body is null)
            //    throw new ArgumentOutOfRangeException($"{nameof(body)} must not be null");

            //if (_isConnected == false)
            //    throw new InvalidOperationException("It hasn't been connected yet");

            //string source = $"{body.Source}:{AppConfiguration.QueueName}";
            //string routingKey = $"{source}.{body.Target.ToString().ToLower()}.{body.Function}";
            //string jsonBody = JsonSerializer.Serialize(body);

            //_channel.BasicPublish("amq.topic", routingKey, body: Encoding.UTF8.GetBytes(jsonBody));
        }
    }
}
