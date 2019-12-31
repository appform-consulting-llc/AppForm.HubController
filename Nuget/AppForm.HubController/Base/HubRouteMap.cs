/*
 * Copyright 2019 AppForm Consulting LLC
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using AppForm.HubController.Attributes;
using AppForm.HubController.Contracts;
using AppForm.HubController.Utils;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace AppForm.HubController.Base
{
    public class HubRouteMap : IHubRouteMap
    {
        private const string HubControllerNameSuffix = "HUBCONTROLLER";

        private readonly ILogger<HubRouteMap> _logger;
        private readonly ConcurrentDictionary<string, HubMethodDescriptor> _routeTable = new ConcurrentDictionary<string, HubMethodDescriptor>();

        public HubRouteMap(ILogger<HubRouteMap> logger)
        {
            _logger = logger;
            var controllerTypes = TypeUtils.GetHubControllerTypes();

            _logger.LogInformation($"Loaded: {controllerTypes.Count} hub controllers");

            foreach (var controllerType in controllerTypes)
            {
                AddController(controllerType);
            }
        }

        private string GetHubMethodRoute(TypeInfo controllerType, MethodInfo handlerMethod) => $"{GetControllerRouteName(controllerType)}/{handlerMethod.Name.ToUpperInvariant()}";

        public HubMethodDescriptor GetRouteHandler(string route)
        {
            try
            {
                var routeKey = _routeTable.Keys.First(k => k.Equals(route.ToUpperInvariant()));
                _routeTable.TryGetValue(routeKey, out var method);

                return method;
            }
            catch(InvalidOperationException ex)
            {
                _logger.LogError(ex, $"Route handler not found: {route}");
                throw;
            }
            catch(ArgumentNullException ex)
            {
                _logger.LogError(ex, $"Route handler not found: {route}");
                throw;
            }
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
            _logger.LogInformation($"Loading hub controller: {controllerType.Name}");

            foreach (var method in controllerType.GetMethods(BindingFlags.Public | BindingFlags.Instance))
            {
                AddControllerMethod(controllerType, method);
            }
        }

        private void AddControllerMethod(TypeInfo controllerType, MethodInfo handlerMethod)
        {
            var route = GetHubMethodRoute(controllerType, handlerMethod);
            _logger.LogDebug($"Loaded hub route: {route}");

            var descriptor = new HubMethodDescriptor(controllerType, handlerMethod);

            _routeTable.TryAdd(route, descriptor);
        }
    }
}
