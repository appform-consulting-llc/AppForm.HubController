using AppForm.HubController.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using AppForm.HubController.Models;

namespace AppForm.HubController
{
    public class AppFormHub : Hub
    {
        private readonly IHubRouter _hubRouter;
        public AppFormHub(IServiceProvider serviceProvider)
        {
            _hubRouter = serviceProvider.GetService<IHubRouter>();
        }

        public virtual async Task AppFormExecute(HubRequest request)
        {
            try
            {
                request.Result = await _hubRouter.HandleRequest(request);
            }
            catch(Exception ex)
            {
                request.Error = ex;
            }

            await Clients.Caller.SendAsync("$AfExecuteResult$", request).ConfigureAwait(false);
        }

    }
}
