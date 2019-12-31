using AppForm.HubController.Base;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AppForm.HubController.Tests.Base
{
    [TestClass]
    public class HubRouteMapTests
    {
        [TestMethod]
        public void HubRouteMap_HasHandler()
        {
            var map = new HubRouteMap(new Mock<ILogger<HubRouteMap>>().Object);

            var handler = map.GetRouteHandler("test/EmptyAsyncIntInputTest");

            Assert.IsNotNull(handler);
        }
    }
}
