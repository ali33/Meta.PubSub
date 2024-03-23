using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Meta.PubSub.Services.Google
{
    public static class GoogleCloudExtensions
    {
        public static IServiceCollection AddGooglePubSub(this IServiceCollection services, IConfiguration config)
        {
            //builder.Services.AddOptions<GooglePubSettings>().Bind(builder.Configuration.GetSection(GooglePubSettings.ConfigSectionName));
            //builder.Services.Configure<GooglePubSettings>(builder.Configuration.GetSection(GooglePubSettings.ConfigSectionName));
            services.AddOptions<GooglePubOptions>().Bind(config.GetSection(GooglePubOptions.SectionName));
            services.AddSingleton<IPublication, GooglePublication>();

          services.AddHostedService<GoogleSubscription>();

            return services;
        }


    }
}
