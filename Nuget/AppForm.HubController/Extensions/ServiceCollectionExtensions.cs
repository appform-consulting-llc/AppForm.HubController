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
