namespace ServiceBusTools
{
    #region Usings
    using Azure.Messaging.ServiceBus;
    using System.Text.Json;
    #endregion

    public class ServiceBusPublisher
    {
        #region Fields
        private const string NOTIFICATION_TOPIC_NAME = "notification";
        private readonly ServiceBusClient serviceBusClient;
        #endregion

        #region Ctor
        public ServiceBusPublisher(ServiceBusClient serviceBusClient)
        {
            this.serviceBusClient = serviceBusClient;
        }
        #endregion

        public async Task PublishNotification<T>(T model, string traceId) where T : class, new()
        {
            await this.Publish(model, NOTIFICATION_TOPIC_NAME, traceId);
        }

        public async Task Publish<T>(T model, string topicName, string traceId) where T : class, new()
        {
            var publisher = serviceBusClient.CreateSender(topicName);
            var body = JsonSerializer.Serialize(new MessageWithTraceId<T>() { Model = model, RequestId = traceId });
            var message = new ServiceBusMessage(body);
            await publisher.SendMessageAsync(message);
        }

        public async Task Publish<T>(T model, string topicName, string traceId, string prefix) where T : class, new()
        {
            var publisher = serviceBusClient.CreateSender(topicName);
            var body = JsonSerializer.Serialize(new MessageWithTraceId<T>() { Model = model, RequestId = traceId });
            var message = new ServiceBusMessage(body);
            message.ApplicationProperties.Add(nameof(prefix), prefix);

            await publisher.SendMessageAsync(message);
        }

        public async Task PublishNotification<T>(T model) where T : class
        {
            await this.Publish(model, NOTIFICATION_TOPIC_NAME);
        }

        public async Task Publish<T>(T model, string topicName) where T : class
        {
            var publisher = serviceBusClient.CreateSender(topicName);
            var body = JsonSerializer.Serialize(model);
            var message = new ServiceBusMessage(body);
            await publisher.SendMessageAsync(message);
        }
    }
}
