using CDPAutomation.Abstracts;
using CDPAutomation.Helpers;
using CDPAutomation.Interfaces.CDP;
using CDPAutomation.Interfaces.JavaScript;
using CDPAutomation.Models.Browser;
using CDPAutomation.Models.CDP;
using CDPAutomation.Models.JavaScript;

namespace CDPAutomation.Implementation
{
    internal class JavaScriptImplementation(ICDP cdp, DebuggerPageResponse debuggerPageResponse) : AbstractInitializeImplementation(cdp, debuggerPageResponse), IJavaScriptExecutor
    {
        private readonly ICDP _cdp = cdp;
        private readonly DebuggerPageResponse _debuggerPageResponse = debuggerPageResponse;

        public Task<object> ExecuteJavaScriptAsync(string script, bool returnValue = false)
        {
            CDPRequest @params = new()
            {
                Method = "Runtime.evaluate",
                Params = new ExecuteJavaScriptParams
                {
                    Expression = script,
                    ReturnByValue = returnValue,
                    TargetId = _debuggerPageResponse.Id,
                }
            };

            Task<CDPResponse?> taskExecuteJavaScript =  _cdp.SendInstantAsync(@params);
            ArgumentNullException.ThrowIfNull(taskExecuteJavaScript);

            if (returnValue)
            {
                CDPResponse? response = taskExecuteJavaScript.Result;
                ExecuteJavaScriptResult? result = response.Deserialize(JsonContext.Default.ExecuteJavaScriptResult);
                ArgumentNullException.ThrowIfNull(result);
            }

            return Task.FromResult<object>(new());
        }
    }
}
