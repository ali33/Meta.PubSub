using Google.Apis.Auth.OAuth2;
using Google.Cloud.PubSub.V1;
using Google.Protobuf;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Grpc.Auth;
namespace Meta.PubSub.Services.Google
{
    public class GooglePublication : IPublication
    {
        readonly GooglePubOptions _options;
        //GoogleCredential defaultCredential = null;
        readonly Dictionary<string, PublisherClient> publisherClients = new Dictionary<string, PublisherClient>();
        public GooglePublication(IOptions<GooglePubOptions> options)
        {
            _options = options.Value; 
            foreach (var channel in _options.PubChannels)
            {
                 if(!channel.Active)
                    continue;
                var credentials = GoogleCredential.FromJson(JsonConvert.SerializeObject(channel.Credentials ?? _options.Credentials));
                var createSettings = new PublisherClient.ClientCreationSettings(credentials: credentials.ToChannelCredentials());
                var topicName = new TopicName(channel.ProjectId, channel.TopicId);
                var publisher = PublisherClient.Create(topicName, clientCreationSettings: createSettings);
                publisherClients[channel.ChannelId] = publisher;
            }

        }

        public string Id { get => "google"; set => throw new NotImplementedException(); }

        public async Task<object> Publish(string channelId, string message)
        {
            var pubMessage = new PubsubMessage()
            {
                Data = ByteString.CopyFromUtf8(message)
            };
            return await publisherClients[channelId].PublishAsync(pubMessage);
        }
    }
}
