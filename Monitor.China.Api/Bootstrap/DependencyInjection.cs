using Microsoft.Extensions.DependencyInjection;
using Monitor.API.Client;
using Monitor.China.Api.MonitorApis.Commands;
using Monitor.China.Api.MonitorApis.Queries;
using Monitor.Ioc;

namespace Monitor.China.Api.Bootstrap
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            MonitorApiProvider.Bootstrap(
                new DependencyRegistrator(services));

            services.AddSingleton<IJsonConverter, JsonConverter>()
                    .AddScoped<ApiTransaction>()
                    .AddScoped<QueryFactory>()
                    .AddScoped<CommandFactory>();

            return services;
        }
    }
}
