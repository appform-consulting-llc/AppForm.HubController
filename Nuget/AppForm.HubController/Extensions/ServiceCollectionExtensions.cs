using AppForm.HubController.Base;
using AppForm.HubController.Contracts;
using AppForm.HubController.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AppForm.HubController.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseHubRouter(this IServiceCollection serviceCollection)
        {
            var hubControllerTypes = TypeUtils.GetHubControllerTypes();
            foreach (var hubControllerType in hubControllerTypes)
            {
                serviceCollection.TryAddScoped(hubControllerType);
            }

            serviceCollection.TryAddSingleton<HubRouteMap>();

            serviceCollection.TryAddScoped<IHubRouter, HubRouter>();

            return serviceCollection;
        }
    }
}
