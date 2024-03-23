using Microsoft.Extensions.Options;

namespace Meta.PubSub.Services.Google
{
    //public class GooglePubOptionsSetup: IConfigureOptions<GooglePubOptions>
    //  {

    //      private readonly IConfiguration _configuration;

    //      public GooglePubOptionsSetup(IConfiguration configuration)
    //      {
    //          _configuration = configuration;
    //      }

    //      public void Configure(GooglePubOptions options)
    //      {
    //          _configuration
    //              .GetSection(SectionName)
    //              .Bind(options);
    //      }
    //  }

    public class GooglePubOptions
    {
        public const string SectionName = "GooglePubOptions";
        public GoogleCredentialContainer Credentials { get; set; }
        public List<GooglePubChannel> PubChannels { get; set; }
        public List<GoogleSubChannel> SubChannels { get; set; }
    }

    public class GooglePubChannel
    {
        public string ChannelId { get; set; }
        public string ProjectId { get; set; }
        public string TopicId { get; set; }
        public GoogleCredentialContainer Credentials { get; set; } = null;
        public bool Active { get; set; }
    }

    public class GoogleSubChannel
    {
        public string ChannelId { get; set; }
        public string ProjectId { get; set; }
        public string SubscriptionId { get; set; }
        public GoogleCredentialContainer Credentials { get; set; } = null;
        public bool Active { get; set; }
    }

    public class GoogleCredentialContainer
    {
        public string type { get; set; }
        public string project_id { get; set; }
        public string private_key_id { get; set; }
        public string private_key { get; set; }
        public string client_email { get; set; }
        public string client_id { get; set; }
        public string auth_uri { get; set; }
        public string token_uri { get; set; }
        public string auth_provider_x509_cert_url { get; set; }
        public string client_x509_cert_url { get; set; }
        public string universe_domain { get; set; }
    }
}