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
using AppForm.HubController.Models;
using Newtonsoft.Json;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace AppForm.HubController
{
    public class HubRouter : IHubRouter
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHubRouteMap _hubRouteMap;
        private readonly MethodInfo _genericExecute;

        public HubRouter(
            IServiceProvider serviceProvider,
            IHubRouteMap hubRouteMap)
        {
            _serviceProvider = serviceProvider;
            _hubRouteMap = hubRouteMap;

            _genericExecute = GetType().GetMethod("ExecuteGenericMethod", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        public async Task<object> HandleRequest(HubRequest hubRequest)
        {
             var methodDescriptor = _hubRouteMap.GetRouteHandler(hubRequest.Route);

            if(typeof(Task).IsAssignableFrom(methodDescriptor.Method.ReturnType))
            {
                var genericUnwrapMethod = _genericExecute.MakeGenericMethod(methodDescriptor.Method.ReturnType.GenericTypeArguments[0]);
                return await (Task<object>)genericUnwrapMethod.Invoke(this, new object[] { hubRequest, methodDescriptor });
            }

            return ExecuteMethod(hubRequest, methodDescriptor);
        }

        private object ExecuteMethod(HubRequest hubRequest, HubMethodDescriptor methodDescriptor)
        {
            var controller = _serviceProvider.GetService(methodDescriptor.ControllerType);

            if (methodDescriptor.ArgumentType != null)
            {
                var convertedArgument = ConvertArguments(hubRequest.Arguments, methodDescriptor);
                return methodDescriptor.Method.Invoke(controller, new[] { convertedArgument });
            }
            else
            {
                return methodDescriptor.Method.Invoke(controller, null);
            }
        }

        private async Task<object> ExecuteGenericMethod<T>(HubRequest hubRequest, HubMethodDescriptor methodDescriptor)
        {
            var controller = _serviceProvider.GetService(methodDescriptor.ControllerType);

            T result;
            if (methodDescriptor.ArgumentType != null)
            {
                var convertedArgument = ConvertArguments(hubRequest.Arguments, methodDescriptor);
                result = await (Task<T>)methodDescriptor.Method.Invoke(controller, new[] { convertedArgument });
            }
            else
            {
                result = await (Task<T>)methodDescriptor.Method.Invoke(controller, null);
            }

            return result;
        }

        private object ConvertArguments(string arguments, HubMethodDescriptor methodDescriptor)
        {
            if (methodDescriptor.ArgumentType.IsAssignableFrom(typeof(string)))
            {
                return arguments;
            }
            if (methodDescriptor.ArgumentType.IsPrimitive || (methodDescriptor.ArgumentType == typeof(decimal)))
            {
                return Convert.ChangeType(arguments, methodDescriptor.ArgumentType);
            }

            return JsonConvert.DeserializeObject(arguments, methodDescriptor.ArgumentType);
        }
        
    }
}
