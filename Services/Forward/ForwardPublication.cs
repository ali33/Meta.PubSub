
using Microsoft.Extensions.Options;
namespace Meta.PubSub.Services.Forward
{
    public class ForwardPublication : IPublication
    {
        readonly ForwardPubOptions _options;
        public ForwardPublication(IOptions<ForwardPubOptions> options)
        {
            _options = options.Value;
        }
        public string Id { get => "forward"; set => throw new NotImplementedException(); }

        public Task<object> Publish(string channelId, string message)
        {
            Console.WriteLine($"Forward {channelId}: {message}");
            return Task.FromResult((object)(new { channelId, message }));
        }
    }
}
