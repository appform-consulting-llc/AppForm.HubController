using AppForm.HubController.Base;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AppForm.HubController.Tests.HubControllers
{
    public class TestHubController : BaseHubController
    {
        public async Task EmptyAsyncIntInputTest(int testId)
        {
            await Task.Delay(10);
        }

        public async Task<int> IntAsyncEmptyInputTest()
        {
            await Task.Delay(10);

            return 1;
        }

        public async Task<int> IntAsyncIntInputTest(int testId)
        {
            await Task.Delay(10);

            return testId;
        }

        public async Task<float> FloatAsyncFloatInputTest(float testId)
        {
            await Task.Delay(10);

            return testId;
        }

        public async Task<double> DoubleAsyncDoubleInputTest(double testId)
        {
            await Task.Delay(10);

            return testId;
        }

        public async Task<decimal> DecimalAsyncDecimalInputTest(decimal testId)
        {
            await Task.Delay(10);

            return testId;
        }

        public async Task<bool> BoolAsyncBoolInputTest(bool testId)
        {
            await Task.Delay(10);

            return testId;
        }

        public async Task<string> StringAsyncStringInputTest(string testId)
        {
            await Task.Delay(10);

            return testId;
        }

        public async Task<TestHubRequest> ObjectAsyncObjectInputTest(TestHubRequest test)
        {
            await Task.Delay(10);

            test.Processed = true;

            return test;
        }
    }
}
