using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace AppForm.HubController.Extensions
{
    public static class ExceptionExtensions
    {
        public static string Serialize(this Exception ex)
        {
            var serializable = new
            {
                Type = ex.GetType().ToString(),
                ex.Message,
                ex.StackTrace
            };

            return JsonConvert.SerializeObject(serializable, new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }
    }
}
