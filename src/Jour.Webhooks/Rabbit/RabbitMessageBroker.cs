using System;
using System.Text;
using Jour.Webhooks.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Jour.Webhooks.Rabbit
{
    public class RabbitMessageBroker : IMessageBroker
    {
        private readonly ILogger<RabbitMessageBroker> _logger;
        private readonly IConnectionFactory _connectionFactory;
        
        public RabbitMessageBroker(IOptions<RabbitOptions> rabbitOptions, ILogger<RabbitMessageBroker> logger)
        {
            _logger = logger;
            RabbitOptions options = rabbitOptions.Value;

            _connectionFactory = new ConnectionFactory()
            {
                HostName = options.Hostname,
                UserName = options.Username,
                Password = options.Password
            };
        }

        public void PublishMessage(string queueName, string message, DateTime date)
        {
            _logger.LogInformation("Prepare publish");
            
            using IConnection connection = _connectionFactory.CreateConnection();
            _logger.LogInformation("Create connection");
            
            using IModel channel = connection.CreateModel();
            _logger.LogInformation("Create channel");
            
            channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            _logger.LogInformation("Declare queue with name \"{QueueName}\"", queueName);
            
            IBasicProperties properties = channel.CreateBasicProperties();
            properties.Persistent = true;
            
            byte[] body = Encoding.UTF8.GetBytes(message);
    
            channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: properties, body: body);
            _logger.LogInformation("Message published to RabbitMQ: \"{JsonMessage}\"", message);
        }
    }
}