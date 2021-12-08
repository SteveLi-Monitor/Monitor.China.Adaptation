using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using System;
using System.Reflection;

namespace Monitor.China.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;

            try
            {
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
                .UseStartup<Startup>();
    }
}
