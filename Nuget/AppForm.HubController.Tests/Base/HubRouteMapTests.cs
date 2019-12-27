using AppForm.HubController.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppForm.HubController.Tests.Base
{
    [TestClass]
    public class HubRouteMapTests
    {
        [TestMethod]
        public void HubRouteMap_HasHandler()
        {
            var map = new HubRouteMap();

            var handler = map.GetRouteHandler("test/EmptyAsyncIntInputTest");

            Assert.IsNotNull(handler);
        }
    }
}
