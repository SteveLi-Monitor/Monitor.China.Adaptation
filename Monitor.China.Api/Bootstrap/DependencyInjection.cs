using Domain.Common;
using Domain.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Monitor.API.Client;
using Monitor.China.Api.MonitorApis.Commands;
using Monitor.China.Api.MonitorApis.Queries;
using Monitor.Ioc;

namespace Monitor.China.Api.Bootstrap
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
        {
            var monitorServerSetting = configuration.GetSection(nameof(MonitorServerSetting))
                .Get<MonitorServerSetting>();
            monitorServerSetting.Guard(nameof(monitorServerSetting));
            monitorServerSetting.Guard();

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
