using AppForm.HubController.Base;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

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

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void HubRouteMap_NoHandler()
        {
            var map = new HubRouteMap(new Mock<ILogger<HubRouteMap>>().Object);

            map.GetRouteHandler("test/EmptyAsyncIntInputTest2");
        }
    }
}
