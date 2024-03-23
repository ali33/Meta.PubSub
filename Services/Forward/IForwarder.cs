
namespace Meta.PubSub.Services.Forward
{
    public interface IForwarder
    {
        string Id { get; set; }
        Task<object> Forward(string channelId, string message);
    }
}
