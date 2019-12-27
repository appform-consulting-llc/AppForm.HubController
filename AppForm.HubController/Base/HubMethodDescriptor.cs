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
