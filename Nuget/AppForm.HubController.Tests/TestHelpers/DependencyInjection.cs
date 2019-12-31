using AppForm.HubController.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AppForm.HubController.Tests.TestHelpers
{
    public static class DependencyInjection
    {
        public static IServiceCollection CreateServiceCollection()
        {
            var collection = new ServiceCollection();
            collection.UseHubRouter();
            collection.AddLogging();

            return collection;
        }

        public static IServiceProvider CreateDependencyInjection(IServiceCollection serviceCollection = null)
        {
            var collection = serviceCollection ?? CreateServiceCollection();
            return collection.BuildServiceProvider();
        }
    }
}
