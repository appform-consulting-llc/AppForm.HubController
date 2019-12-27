using AppForm.HubController.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppForm.HubController.Tests.Utils
{
    [TestClass]
    public class TypeUtilsTests
    {
        [TestMethod]
        public void GetHubControllerTypes_ReturnsValues()
        {
            var controllerTypes = TypeUtils.GetHubControllerTypes();

            Assert.IsNotNull(controllerTypes);
            Assert.IsTrue(controllerTypes.Count > 0);
        }
    }
}
