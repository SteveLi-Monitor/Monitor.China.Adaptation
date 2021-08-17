using Microsoft.Extensions.DependencyInjection;
using Monitor.Ioc;

namespace Monitor.China.Api.Bootstrap
{
    public class DependencyRegistrator : IRegistrator
    {
        private readonly IServiceCollection services;

        public DependencyRegistrator(IServiceCollection services)
        {
            this.services = services;
        }

        public void RegisterSingleton<TService>(TService instance)
            where TService : class
        {
            services.AddSingleton(instance);
        }

        public void RegisterSingleton<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            services.AddSingleton<TService, TImplementation>();
        }
    }
}
