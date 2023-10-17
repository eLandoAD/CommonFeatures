namespace ServiceBusTools
{
    public class MessageWithTraceId<T> where T : class, new()
    {
        public Guid TraceId { get; set; }
        public T Model { get; set; } = new T();
    }
}
