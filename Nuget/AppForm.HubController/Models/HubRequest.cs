using System;

namespace AppForm.HubController.Models
{
    public class HubRequest
    {
        public string Route { get; set; }

        public string CallbackId { get; set; }

        public string Arguments { get; set; }

        public object Result { get; set; }

        public Exception Error { get; set; }
    }
}
