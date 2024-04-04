namespace ServiceBusTools
{
    #region Usings
    using Azure.Messaging.ServiceBus;
    using System.Text.Json;
    #endregion

    public class ServiceBusPublisher
    {
        #region Fields
        private readonly ServiceBusClient serviceBusClient;
        #endregion

        #region Ctor
        public ServiceBusPublisher(ServiceBusClient serviceBusClient)
        {
            this.serviceBusClient = serviceBusClient;
        }
        #endregion

        public async Task Publish<T>(T model, string topicName, string traceId, string? userId = null)
            where T : class, new()
        {
            var publisher = serviceBusClient.CreateSender(topicName);
            var body = JsonSerializer.Serialize(new MessageWithMetaData<T>(model, traceId, userId));
            var message = new ServiceBusMessage(body);
            await publisher.SendMessageAsync(message);
        }

        public async Task Publish<T>(T model, string topicName, string traceId, string? userId = null, string prefix = "")
           where T : class, new()
        {
            var publisher = serviceBusClient.CreateSender(topicName);
            var body = JsonSerializer.Serialize(new MessageWithMetaData<T>(model, traceId, userId));
            var message = new ServiceBusMessage(body);
            message.ApplicationProperties.Add(nameof(prefix), prefix);

            await publisher.SendMessageAsync(message);
        }

        public async Task Publish<T>(T model, string topicName)
            where T : class
        {
            var publisher = serviceBusClient.CreateSender(topicName);
            var body = JsonSerializer.Serialize(model);
            var message = new ServiceBusMessage(body);
            await publisher.SendMessageAsync(message);
        }

        public async Task Publish<T>(T model, string topicName, string prefix)
           where T : class
        {
            var publisher = serviceBusClient.CreateSender(topicName);
            var body = JsonSerializer.Serialize(model);
            var message = new ServiceBusMessage(body);
            message.ApplicationProperties.Add(nameof(prefix), prefix);

            await publisher.SendMessageAsync(message);
        }
    }
}
