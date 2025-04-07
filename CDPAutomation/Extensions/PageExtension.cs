using CDPAutomation.Helpers;
using CDPAutomation.Interfaces.CDP;
using CDPAutomation.Models.Browser;
using CDPAutomation.Models.CDP;
using CDPAutomation.Models.Page;

namespace CDPAutomation.Extensions
{
    internal static class PageExtension
    {
        internal static Task<DebuggerPageResult?> GetTargetInfo(this ICDP cdp, string? targetId)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(targetId);

            CDPRequest @params = new()
            {
                Method = "Target.getTargetInfo",
                Params = new PageTargetIdInfoParams
                {
                    TargetId = targetId
                }
            };

            Task<CDPResult?>? taskGetTargetInfo = cdp.SendInstantAsync(@params);
            ArgumentNullException.ThrowIfNull(taskGetTargetInfo);

            CDPResult? response = taskGetTargetInfo.Result;
            ArgumentNullException.ThrowIfNull(response);

            TargetInfoObjectResult? resultTargetInfoObjectResult = response.Deserialize(JsonContext.Default.TargetInfoObjectResult);
            ArgumentNullException.ThrowIfNull(resultTargetInfoObjectResult);
            ArgumentNullException.ThrowIfNull(resultTargetInfoObjectResult.TarGetInfo);

            DebuggerPageResult debuggerPageResponse = new()
            {
                Id = resultTargetInfoObjectResult.TarGetInfo.TargetId,
                Title = resultTargetInfoObjectResult.TarGetInfo.Title,
                Url = resultTargetInfoObjectResult.TarGetInfo.Url,
                Type = resultTargetInfoObjectResult.TarGetInfo.Type,
                //Description = resultTargetInfoObjectResult.TarGetInfo.Description,
                //Attached = resultTargetInfoObjectResult.TarGetInfo.Attached,
                //OpenerId = resultTargetInfoObjectResult.TarGetInfo.OpenerId,
                //BrowserContextId = resultTargetInfoObjectResult.TarGetInfo.BrowserContextId,
                //IsAutoAttach = resultTargetInfoObjectResult.TarGetInfo.IsAutoAttach,
                //CanAccessOpener = resultTargetInfoObjectResult.TarGetInfo.CanAccessOpener
            };

            return Task.FromResult<DebuggerPageResult?>(debuggerPageResponse);
        }

        internal static Task<DebuggerPageResult?> GetTargetInfo(this ICDP cdp, NewPageResult? pageResult)
        {
            ArgumentNullException.ThrowIfNull(pageResult);

            string? targetId = pageResult.TargetId;

            Task<DebuggerPageResult?> taskGetTargetInfo = cdp.GetTargetInfo(targetId);
            ArgumentNullException.ThrowIfNull(taskGetTargetInfo);

            DebuggerPageResult? result = taskGetTargetInfo.Result;
            ArgumentNullException.ThrowIfNull(result);

            return Task.FromResult<DebuggerPageResult?>(result);
        }
    }
}
