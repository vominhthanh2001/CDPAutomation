using CDPAutomation.Interfaces.CDP;
using CDPAutomation.Interfaces.JavaScript;

namespace CDPAutomation.Implementation
{
    public class JavaScriptExecutorImplementation : IJavaScriptExecutor
    {
        private readonly ICDP _cdp;
        public JavaScriptExecutorImplementation(ICDP cdp)
        {
            _cdp = cdp;
        }
        
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
