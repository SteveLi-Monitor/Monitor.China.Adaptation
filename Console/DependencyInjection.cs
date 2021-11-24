using Console.ImportServices;
using Console.Models.ClassMaps;
using Microsoft.Extensions.DependencyInjection;

namespace Console
{
    internal static class DependencyInjection
    {
        public static IServiceCollection AddClassMaps(this IServiceCollection services)
        {
            return services.AddTransient<PartMap>();
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddSingleton<ServiceResolver>()
                .AddTransient<PartImport>();
        }
    }
}
