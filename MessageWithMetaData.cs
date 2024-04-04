namespace ServiceBusTools
{
    public record MessageWithMetaData<T>(T Model, string RequestId, string? UserId = null) where T : class;
}
