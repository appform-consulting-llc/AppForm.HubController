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

using System;
using System.Reflection;

namespace AppForm.HubController.Base
{
    public class HubMethodDescriptor
    {
        public TypeInfo ControllerType { get; private set; }

        public MethodInfo Method { get; private set; }

        public Type ArgumentType { get; private set; }

        public HubMethodDescriptor(TypeInfo controllerType, MethodInfo methodInfo)
        {
            ControllerType = controllerType;
            Method = methodInfo;

            var methodParams = Method.GetParameters();
            if(methodParams.Length > 1)
            {
                throw new ArgumentException("HubController method cannot have more than 1 argument");
            }

            if(methodParams.Length > 0)
            {
                ArgumentType = methodParams[0].ParameterType;
            }
        }
    }
}
