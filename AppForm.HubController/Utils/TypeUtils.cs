using AppForm.HubController.Base;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AppForm.HubController.Utils
{
    public static class TypeUtils
    {
        public static IList<TypeInfo> GetHubControllerTypes()
        {
            return Assembly
                .GetEntryAssembly()
                .DefinedTypes
                .Where(t => typeof(BaseHubController).IsAssignableFrom(t) && typeof(BaseHubController) != t.UnderlyingSystemType)
                .ToList();
        }
    }
}
