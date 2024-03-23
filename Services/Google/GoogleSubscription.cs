using System.Net;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.PubSub.V1;
using Google.Protobuf;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Grpc.Auth;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Extensions.Hosting;

namespace Meta.PubSub.Services.Google
{
    public class GoogleSubscription : IHostedService, IDisposable
    {
        readonly GooglePubOptions _options;
        //GoogleCredential defaultCredential = null;
        readonly Dictionary<string, SubscriberClient> subscriberClients = new Dictionary<string, SubscriberClient>();
        public GoogleSubscription(IOptions<GooglePubOptions> options)
        {
            _options = options.Value;
            foreach (var channel in _options.SubChannels)
            {
                if (!channel.Active)
                    continue;
                var credentials = GoogleCredential.FromJson(JsonConvert.SerializeObject(channel.Credentials ?? _options.Credentials));
                var createSettings = new SubscriberClient.ClientCreationSettings(credentials: credentials.ToChannelCredentials());
                var subscriptionName = new SubscriptionName(channel.ProjectId, channel.SubscriptionId);
                var subscriber = SubscriberClient.Create(subscriptionName, clientCreationSettings: createSettings);
                subscriberClients[channel.ChannelId] = subscriber;
            }

        }

        public string Id { get => "google"; set => throw new NotImplementedException(); }

        public void Dispose()
        {
            foreach (var subscriber in subscriberClients.Values)
            {
                subscriber.DisposeAsync();
            }
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            foreach (var subId in subscriberClients.Keys)
            {
                SubscriberClient subscription;
                if (!subscriberClients.TryGetValue(subId, out subscription))
                    continue;
                subscription.StartAsync(subscriptionHandler);
            }
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            foreach (var subscriber in subscriberClients.Values)
            {
                subscriber.StopAsync(CancellationToken.None);
            }
            return Task.CompletedTask;
        }

        private async Task<SubscriberClient.Reply> subscriptionHandler(PubsubMessage message, CancellationToken token)
        {
            string id = message.MessageId;
            string publishDate = message.PublishTime.ToDateTime().ToString("dd-M-yyyy HH:MM:ss");
            string data = message.Data.ToStringUtf8(); 
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine($" id: {id}\r\n date: {publishDate}\r\n data: {data}"); 
            return await Task.FromResult(SubscriberClient.Reply.Ack);
        }
    }
}
