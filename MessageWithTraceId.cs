namespace ServiceBusTools
{
    public class MessageWithTraceId<T> where T : class, new()
    {
        public string? UserId { get; set; }
        public string RequestId { get; set; } = "";
        public T Model { get; set; } = new T();
    }
}
