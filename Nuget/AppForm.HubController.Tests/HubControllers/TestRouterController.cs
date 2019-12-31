using AppForm.HubController.Attributes;
using AppForm.HubController.Base;
using System.Threading.Tasks;

namespace AppForm.HubController.Tests.HubControllers
{
    [Route("router")]
    public class TestRouterController : BaseHubController
    {
        public async Task EmptyTask()
        {
            await Task.Delay(10);
        }
    }
}
