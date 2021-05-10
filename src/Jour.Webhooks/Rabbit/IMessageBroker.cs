namespace Jour.Webhooks.Rabbit
{
    public interface IMessageBroker
    {
        void PublishMessage(string queueName, string message);
    }
}