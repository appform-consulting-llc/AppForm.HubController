using AppForm.HubController.Contracts;
using AppForm.HubController.Models;
using AppForm.HubController.Tests.TestHelpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace AppForm.HubController.Tests.Attributes
{
    [TestClass]
    public class RouteAttributeTests
    {
        [TestMethod]
        public async Task EmptyTaskTest()
        {
            var request = new HubRequest
            {
                Route = "router/EmptyTask"
            };

            var result = await HubRouter.HandleRequest(request);

            Assert.IsNull(result);
        }

        private IHubRouter HubRouter => DependencyInjection.CreateDependencyInjection().GetService<IHubRouter>();

    }
}
