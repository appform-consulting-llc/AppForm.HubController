using System;

namespace AppForm.HubController.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RouteAttribute : Attribute
    {
        public string Route { get; private set; }

        public RouteAttribute(string route)
        {
            Route = route;
        }
    }
}
