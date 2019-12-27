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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AppForm.HubController.Utils
{
    public static class TypeUtils
    {
        private static IList<TypeInfo> _hubControllerList;

        public static IList<TypeInfo> GetHubControllerTypes()
        {
            if(_hubControllerList == null)
            {
                _hubControllerList = AppDomain.CurrentDomain.GetAssemblies()
                 .SelectMany(GetTypes)
                 .Where(t => typeof(BaseHubController).IsAssignableFrom(t) && typeof(BaseHubController) != t.UnderlyingSystemType)
                 .Select(t => t.GetTypeInfo())
                 .ToList();
            }

            return _hubControllerList;
        }

        private static Type[] GetTypes(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (Exception)
            {
                return new Type[] { };
            }
        }
    }
}
