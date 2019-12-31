using AppForm.HubController.Extensions;
using AppForm.HubController.Models;
using AppForm.HubController.Tests.TestHelpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace AppForm.HubController.Tests
{
    [TestClass]
    public class AppFormHubTests
    {
        [TestMethod]
        public async Task AppFormExecute_InvalidRoute_Succeeds_WithError()
        {
            var hub = Hub;
            var request = new HubRequest
            {
                Route = "fake/route/here"
            };

            await hub.AppFormExecute(request);

            Assert.IsNotNull(request.Error);
        }

        private IServiceCollection ServiceCollection => DependencyInjection.CreateServiceCollection().AddSingleton<AppFormHub>();

        private AppFormHub Hub => DependencyInjection.CreateDependencyInjection(ServiceCollection).GetService<AppFormHub>();
    }
}
