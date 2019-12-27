using AppForm.HubController.Attributes;
using AppForm.HubController.Contracts;
using AppForm.HubController.Utils;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace AppForm.HubController.Base
{
    public class HubRouteMap : IHubRouteMap
    {
        private const string HubControllerNameSuffix = "HUBCONTROLLER";

        private readonly ConcurrentDictionary<string, HubMethodDescriptor> _routeTable = new ConcurrentDictionary<string, HubMethodDescriptor>();

        public HubRouteMap()
        {
            var controllerTypes = TypeUtils.GetHubControllerTypes();

            foreach (var controllerType in controllerTypes)
            {
                AddController(controllerType);
            }
        }

        private string GetHubMethodRoute(TypeInfo controllerType, MethodInfo handlerMethod) => $"{GetControllerRouteName(controllerType)}/{handlerMethod.Name.ToUpperInvariant()}";

        public HubMethodDescriptor GetRouteHandler(string route)
        {
            var routeKey = _routeTable.Keys.First(k => k.Equals(route.ToUpperInvariant()));
            _routeTable.TryGetValue(routeKey, out var method);

            return method;
        }
        
        private string GetControllerRouteName(TypeInfo controllerType)
        {
            var controllerName = controllerType.Name.ToUpperInvariant();
            if (controllerName.EndsWith(HubControllerNameSuffix))
            {
                controllerName = controllerName.Substring(0, controllerName.IndexOf(HubControllerNameSuffix));
            }

            var routeAttribute = controllerType.GetCustomAttribute<RouteAttribute>();
            if (routeAttribute != null)
            {
                var attributeRoute = routeAttribute.Route.ToUpperInvariant();
                controllerName = attributeRoute.Replace("[CONTROLLER]", controllerName);
            }

            return controllerName;
        }

        private void AddController(TypeInfo controllerType)
        {
            foreach (var method in controllerType.DeclaredMethods)
            {
                AddControllerMethod(controllerType, method);
            }
        }

        private void AddControllerMethod(TypeInfo controllerType, MethodInfo handlerMethod)
        {
            var route = GetHubMethodRoute(controllerType, handlerMethod);
            var descriptor = new HubMethodDescriptor(controllerType, handlerMethod);

            _routeTable.TryAdd(route, descriptor);
        }
    }
}
