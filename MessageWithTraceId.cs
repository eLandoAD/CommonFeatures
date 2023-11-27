namespace ServiceBusTools
{
    public class MessageWithTraceId<T> where T : class, new()
    {
        public string TraceId { get; set; } = "";
        public T Model { get; set; } = new T();
    }
}
