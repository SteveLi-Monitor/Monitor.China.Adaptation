using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Console
{
    internal static class Program
    {
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
            var configuration = BuildConfiguration();
            BuildSerilog(configuration);
            ConfigureServices(services);
        }

        private static IConfiguration BuildConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", false)
                .Build();
        }

        private static void BuildSerilog(IConfiguration configuration)
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
            services.AddSingleton<App>();
        }
    }
}
