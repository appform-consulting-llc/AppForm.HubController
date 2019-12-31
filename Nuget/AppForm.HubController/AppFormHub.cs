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

using AppForm.HubController.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using AppForm.HubController.Models;
using AppForm.HubController.Extensions;

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
                request.Error = ex.Serialize();
            }

            if(Clients?.Caller == null)
            {
                return;
            }

            await Clients.Caller.SendAsync("$AfExecuteResult$", request).ConfigureAwait(false);
        }

    }
}
