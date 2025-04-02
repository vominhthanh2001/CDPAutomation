using CDPAutomation.Implementation;
using CDPAutomation.Interfaces.CDP;
using CDPAutomation.Interfaces.Pages;
using CDPAutomation.Models.CDP;
using CDPAutomation.Models.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CDPAutomation.Helpers
{
    internal static class PageHelper
    {
        internal static async Task<TargetInfo?> GetPageInfoByIdAsync(this ICDP cdp, string? targetId)
        {
            if (cdp is null) throw new ArgumentNullException(nameof(cdp));
            if (string.IsNullOrWhiteSpace(targetId)) throw new ArgumentNullException(nameof(targetId));

            string? getTargets = await cdp.SendInstantAsync(
                method: "Target.getTargets",
                parameters: new object());

            if (getTargets is not null)
            {
                CDPResponse? response = JsonHelper.Deserialize(getTargets, JsonContext.Default.CDPResponse);
                if (response is null || response?.Result is null) return default;

                TargetPages? targetPages = JsonHelper.Deserialize(response?.Result?.ToString() ?? throw new Exception(), JsonContext.Default.TargetPages);
                if (targetPages is null || targetPages?.TargetInfos is null) return default;

                TargetInfo? targetInfo = targetPages.TargetInfos.FirstOrDefault(t => t.TargetId == targetId);
                return targetInfo;
            }

            return default;
        }

        internal static async Task<TargetInfo?> GetPageInfoTargetAsync(this ICDP cdp)
        {
            if (cdp is null) throw new ArgumentNullException(nameof(cdp));

            string? getTargets = await cdp.SendInstantAsync(
                method: "Target.getTargets",
                parameters: new object());

            if (getTargets is not null)
            {
                CDPResponse? response = JsonHelper.Deserialize(getTargets, JsonContext.Default.CDPResponse);
                if (response is null || response?.Result is null) return default;

                TargetPages? targetPages = JsonHelper.Deserialize(response?.Result?.ToString() ?? throw new Exception(), JsonContext.Default.TargetPages);
                if (targetPages is null || targetPages?.TargetInfos is null) return default;

                TargetInfo? targetInfo = targetPages.TargetInfos.FirstOrDefault(t => t.Attached);
                return targetInfo;
            }

            return default;
        }

        internal static async Task<List<TargetInfo>> GetPagesInfoAsync(this ICDP cdp)
        {
            if (cdp is null) throw new ArgumentNullException(nameof(cdp));

            string? getTargets = await cdp.SendInstantAsync(
                method: "Target.getTargets",
                parameters: new object());

            if (getTargets is not null)
            {
                CDPResponse? response = JsonHelper.Deserialize(getTargets, JsonContext.Default.CDPResponse);
                if (response is null || response?.Result is null) return new List<TargetInfo>();

                TargetPages? targetPages = JsonHelper.Deserialize(response?.Result?.ToString() ?? throw new Exception(), JsonContext.Default.TargetPages);
                if (targetPages is null || targetPages?.TargetInfos is null) return new List<TargetInfo>();

                List<TargetInfo>? targetInfos = targetPages.TargetInfos.Where(t => t.Type == "page").ToList();
                if (targetInfos is null) return new List<TargetInfo>();

                targetInfos.Reverse();
                return targetInfos;
            }

            return new List<TargetInfo>();
        }
    }
}
