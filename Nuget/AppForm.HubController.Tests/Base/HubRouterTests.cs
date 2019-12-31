using AppForm.HubController.Contracts;
using AppForm.HubController.Extensions;
using AppForm.HubController.Models;
using AppForm.HubController.Tests.HubControllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppForm.HubController.Tests.Base
{
    [TestClass]
    public class HubRouterTests
    {
        [TestMethod]
        public async Task HandleRequest_AsyncInt_NoParam_Success()
        {
            var hubRouter = HubRouter;

            var request = new HubRequest
            {
                Route = "Test/IntAsyncEmptyInputTest"
            };

            var result = await hubRouter.HandleRequest(request);

            Assert.IsNotNull(result);
            Assert.IsTrue(result is int);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public async Task HandleRequest_AsyncInt_IntParam_Success()
        {
            var hubRouter = HubRouter;
            var expectedResult = 12;
            var request = new HubRequest
            {
                Route = "Test/IntAsyncIntInputTest",
                Arguments = expectedResult.ToString()
            };

            var result = await hubRouter.HandleRequest(request);

            Assert.IsNotNull(result);
            Assert.IsTrue(result is int);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public async Task HandleRequest_AsyncFloat_FloatParam_Success()
        {
            var hubRouter = HubRouter;
            var expectedResult = 12.2f;
            var request = new HubRequest
            {
                Route = "Test/FloatAsyncFloatInputTest",
                Arguments = expectedResult.ToString()
            };

            var result = await hubRouter.HandleRequest(request);

            Assert.IsNotNull(result);
            Assert.IsTrue(result is float);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public async Task HandleRequest_AsyncDouble_DoubleParam_Success()
        {
            var hubRouter = HubRouter;
            var expectedResult = 12.4d;
            var request = new HubRequest
            {
                Route = "Test/DoubleAsyncDoubleInputTest",
                Arguments = expectedResult.ToString()
            };

            var result = await hubRouter.HandleRequest(request);

            Assert.IsNotNull(result);
            Assert.IsTrue(result is double);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public async Task HandleRequest_AsyncDecimal_DecimalParam_Success()
        {
            var hubRouter = HubRouter;
            var expectedResult = 22.4m;
            var request = new HubRequest
            {
                Route = "Test/DecimalAsyncDecimalInputTest",
                Arguments = expectedResult.ToString()
            };

            var result = await hubRouter.HandleRequest(request);

            Assert.IsNotNull(result);
            Assert.IsTrue(result is decimal);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public async Task HandleRequest_AsyncBool_BoolParam_Success()
        {
            var hubRouter = HubRouter;
            var expectedResult = true;
            var request = new HubRequest
            {
                Route = "Test/BoolAsyncBoolInputTest",
                Arguments = expectedResult.ToString()
            };

            var result = await hubRouter.HandleRequest(request);

            Assert.IsNotNull(result);
            Assert.IsTrue(result is bool);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public async Task HandleRequest_AsyncString_StringParam_Success()
        {
            var hubRouter = HubRouter;
            var expectedResult = "some test value";
            var request = new HubRequest
            {
                Route = "Test/StringAsyncStringInputTest",
                Arguments = expectedResult
            };

            var result = await hubRouter.HandleRequest(request);

            Assert.IsNotNull(result);
            Assert.IsTrue(result is string);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public async Task HandleRequest_AsyncObject_ObjectParam_Success()
        {
            var hubRouter = HubRouter;
            var expectedResult = new TestHubRequest
            {
                IntValue = 12,
                StringValue = "test string"
            };
            var request = new HubRequest
            {
                Route = "Test/ObjectAsyncObjectInputTest",
                Arguments = JsonConvert.SerializeObject(expectedResult)
            };

            Assert.IsFalse(expectedResult.Processed);

            var result = await hubRouter.HandleRequest(request);

            Assert.IsNotNull(result);
            Assert.IsTrue(result is TestHubRequest);
            Assert.IsTrue(((TestHubRequest)result).Processed);
            Assert.AreEqual(expectedResult.StringValue, ((TestHubRequest)result).StringValue);
        }

        private IHubRouter HubRouter => CreateDependencyInjection().GetService<IHubRouter>();

        private IServiceProvider CreateDependencyInjection()
        {
            var collection = new ServiceCollection();
            collection.UseHubRouter();
            collection.AddLogging();

            return collection.BuildServiceProvider();
        }
    }
}
