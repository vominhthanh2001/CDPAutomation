using CDPAutomation.Abstracts;
using CDPAutomation.Helpers;
using CDPAutomation.Interfaces.CDP;
using CDPAutomation.Interfaces.JavaScript;
using CDPAutomation.Models.Browser;
using CDPAutomation.Models.CDP;
using CDPAutomation.Models.JavaScript;

namespace CDPAutomation.Implementation
{
    internal class JavaScriptImplementation(ICDP cdp, DebuggerPageResult debuggerPageResponse) : AbstractInitializeImplementation(cdp, debuggerPageResponse), IJavaScriptExecutor
    {
        private readonly ICDP _cdp = cdp;
        private readonly DebuggerPageResult _debuggerPageResponse = debuggerPageResponse;

        public async Task<object?> ExecuteJavaScriptAsync(string script, bool returnValue = false)
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

            CDPResult? response = await _cdp.SendInstantAsync(@params);
            ArgumentNullException.ThrowIfNull(response);

            if (returnValue)
            {
                string? result = JsonHelper.GetProperty(response.Result.ToString(), "result");
                ArgumentNullException.ThrowIfNullOrWhiteSpace(result);

                ExecuteJavaScriptResult? executeJavaScriptResult = JsonHelper.Deserialize(result, JsonContext.Default.ExecuteJavaScriptResult);
                ArgumentNullException.ThrowIfNull(executeJavaScriptResult);

                return executeJavaScriptResult.Value;
            }

            return Task.FromResult<object>(new());
        }
    }
}
