using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Meta.PubSub.Services.Forward
{
    public static class ForwardExtensions
    {
        public static IServiceCollection AddForwardPubSub(this IServiceCollection services, IConfiguration config)
        {
            services.AddOptions<ForwardPubOptions>().Bind(config.GetSection(ForwardPubOptions.SectionName));
            services.AddSingleton<IPublication, ForwardPublication>();
            return services;
        }
    }
}
