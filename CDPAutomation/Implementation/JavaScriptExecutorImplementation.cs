using CDPAutomation.Interfaces.JavaScript;

namespace CDPAutomation.Implementation
{
    public class JavaScriptExecutorImplementation : IJavaScriptExecutor
    {
        public Task ExecuteJavaScriptAsync(string script)
        {
            throw new NotImplementedException();
        }

        public Task<T> ExecuteJavaScriptAsync<T>(string script)
        {
            throw new NotImplementedException();
        }
    }
}
