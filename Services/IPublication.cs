namespace Meta.PubSub.Services
{
    public interface IPublication
    {
        string Id { get; set; }
        Task<object> Publish(string channelId, string message);
    }
}
