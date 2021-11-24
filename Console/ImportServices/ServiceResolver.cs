using Microsoft.Extensions.DependencyInjection;
using System;

namespace Console.ImportServices
{
    internal class ServiceResolver
    {
        private readonly IServiceProvider serviceProvider;

        public ServiceResolver(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IImportService Resolve(string name)
        {
            switch (name)
            {
                case nameof(PartImport):
                    return serviceProvider.GetService<PartImport>();

                default:
                    throw new InvalidOperationException($"Failed to resolve {nameof(IImportService)} {name}.");
            }
        }
    }
}
