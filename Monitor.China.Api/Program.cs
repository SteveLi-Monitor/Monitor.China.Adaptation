using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.Reflection;

namespace Monitor.China.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(AppDomain.CurrentDomain.BaseDirectory + "Logs/start-up.txt")
                .CreateLogger();

            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;

            try
            {
                Log.Information($"Starting web host: {assemblyName}.");
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception e)
            {
                Log.Fatal(e, $"Web host terminated: {assemblyName}.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseSerilog((context, configuration) =>
                {
                    configuration
                        .ReadFrom.Configuration(context.Configuration)
                        .Enrich.FromLogContext()
                        .WriteTo.Console()
                        .WriteTo.File(
                            AppDomain.CurrentDomain.BaseDirectory + "Logs/log.txt",
                            rollingInterval: RollingInterval.Day);
                })
                .UseStartup<Startup>();
    }
}
