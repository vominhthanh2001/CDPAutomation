using CDPAutomation.Interfaces.CDP;
using CDPAutomation.Interfaces.JavaScript;
using CDPAutomation.Models.JavaScript;

namespace CDPAutomation.Implementation
{
    public class JavaScriptExecutorImplementation : IJavaScriptExecutor
    {
        private readonly ICDP _cdp;
        public JavaScriptExecutorImplementation(ICDP cdp)
        {
            _cdp = cdp;
        }
        
        public async Task ExecuteJavaScriptAsync(string script)
        {
            JavaScriptParams javaScriptParams = new JavaScriptParams
            {
                Expression = script
            };

            await _cdp.SendAsync("Runtime.evaluate", javaScriptParams);
        }
    }
}
