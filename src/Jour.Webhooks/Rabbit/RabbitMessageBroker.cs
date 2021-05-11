using System;
using System.Text;
using System.Text.Json;
using Jour.Webhooks.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Jour.Webhooks.Rabbit
{
    public class RabbitMessageBroker : IMessageBroker
    {
        private readonly ILogger<RabbitMessageBroker> _logger;
        private readonly IModel _channel;

        public RabbitMessageBroker(ConnectionFactory connectionFactory, IOptions<RabbitOptions> rabbitOptions,
            ILogger<RabbitMessageBroker> logger)
        {
            _logger = logger;
            RabbitOptions options = rabbitOptions.Value;

            connectionFactory.HostName = options.Hostname;
            connectionFactory.UserName = options.Username;
            connectionFactory.Password = options.Password;

            _logger.LogInformation("Creating connection");
            IConnection connection = connectionFactory.CreateConnection();
            connection.ConnectionShutdown += (sender, args) => _logger.LogInformation("Connection shutdown");

            _logger.LogInformation("Creating channel");
            _channel = connection.CreateModel();

            _channel.ModelShutdown += (sender, args) => _logger.LogInformation("Model shutdown");
        }

        public void PublishMessage(string queueName, string message, DateTime date)
        {
            _logger.LogInformation("Preparing to publish message.\nDeclaring queue with name \"{QueueName}\"",
                queueName);
            _channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false,
                arguments: null);

            IBasicProperties properties = _channel.CreateBasicProperties();
            properties.Persistent = true;

            string json = JsonSerializer.Serialize(new WorkoutMessage(message, date.ToString("O")));
            byte[] body = Encoding.UTF8.GetBytes(json);

            _logger.LogInformation("Publishing message");
            _channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: properties, body: body);

            _logger.LogInformation("Message published to RabbitMQ: \"{JsonMessage}\"", message);
        }
    }
}