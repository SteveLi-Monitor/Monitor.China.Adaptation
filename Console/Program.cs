using Console.MonitorApis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Console
{
    internal static class Program
    {
        private static IConfiguration configuration;

        private static async Task Main(string[] args)
        {
            var services = new ServiceCollection();

            BootstrapApp(services);

            using var serviceProvider = services.BuildServiceProvider();
            var app = serviceProvider.GetRequiredService<App>();

            await app.StartAsync();
        }

        private static void BootstrapApp(IServiceCollection services)
        {
            BuildConfiguration();
            BuildSerilog();
            ConfigureServices(services);
        }

        private static void BuildConfiguration()
        {
            configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("Configs/appsettings.json", false)
                .AddJsonFile("Configs/CsvMapper.json", false)
                .Build();
        }

        private static void BuildSerilog()
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.Console()
                .WriteTo.File(
                    AppDomain.CurrentDomain.BaseDirectory + "Logs/log.txt",
                    rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(configuration)
                .AddClassMaps()
                .AddServices()
                .AddSingleton<MonitorApiService>()
                .AddTransient<App>();
        }
    }
}
