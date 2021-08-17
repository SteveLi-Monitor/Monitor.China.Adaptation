using Microsoft.Extensions.DependencyInjection;
using Monitor.API.Client;
using Monitor.Ioc;

namespace Monitor.China.Api.Bootstrap
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            MonitorApiProvider.Bootstrap(
                new DependencyRegistrator(services));

            services.AddSingleton<IJsonConverter, JsonConverter>();

            return services;
        }
    }
}
