namespace Meta.PubSub.Services.Forward
{
    //public class ForwardPubOptionsSetup : IConfigureOptions<ForwardPubOptions>
    //{
    //    private const string SectionName = "ForwardPubOptions";
    //    private readonly IConfiguration _configuration;

    //    public ForwardPubOptionsSetup(IConfiguration configuration)
    //    {
    //        _configuration = configuration;
    //    }

    //    public void Configure(ForwardPubOptions options)
    //    {
    //        _configuration
    //            .GetSection(SectionName)
    //            .Bind(options);
    //    }
    //}

    public class ForwardPubOptions
    {
         public const string SectionName = "ForwardPubOptions";
    }
}
