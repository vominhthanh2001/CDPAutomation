using CDPAutomation.Abstracts;
using CDPAutomation.Helpers;
using CDPAutomation.Implementation.Events;
using CDPAutomation.Interfaces.Browser;
using CDPAutomation.Interfaces.CDP;
using CDPAutomation.Models.Browser;
using CDPAutomation.Models.CDP;
using CDPAutomation.Models.Navigate;
using CDPAutomation.Models.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Implementation
{
    internal class NavigateImplementation(ICDP cdp, DebuggerPageResult debuggerPageResponse) : AbstractInitializeImplementation(cdp, debuggerPageResponse), INavigate
    {
        private readonly ICDP _cdp = cdp;
        private readonly DebuggerPageResult _debuggerPageResponse = debuggerPageResponse;

        public async Task GoToBackAsync(OptionNavigateModel? option = null)
        {
            option ??= new OptionNavigateModel();

            CDPRequest @params = new()
            {
                Method = "Page.getNavigationHistory",
                Params = new NavigationHistoryParams
                {
                    TargetId = _debuggerPageResponse.Id
                }
            };

            CDPResult? response = await _cdp.SendInstantAsync(@params);
            ArgumentNullException.ThrowIfNull(response);

            NavigationHistoryResult? navigationHistoryResult = response.Deserialize(JsonContext.Default.NavigationHistoryResult, true);
            ArgumentNullException.ThrowIfNull(navigationHistoryResult);

            int? currentIndex = navigationHistoryResult.CurrentIndex;
            ArgumentNullException.ThrowIfNull(currentIndex);

            List<NavigationHistoryEntriesResult>? entries = navigationHistoryResult.entries;
            ArgumentNullException.ThrowIfNull(entries);

            if (currentIndex <= 0 || currentIndex - 1 <= 0) return;

            NavigationHistoryEntriesResult entry = entries[currentIndex.Value - 1];
            ArgumentNullException.ThrowIfNull(entry);

            await GoToUrlAsync(entry.Url!, option);
        }

        public async Task GoToForwardAsync(OptionNavigateModel? option = null)
        {
            option ??= new OptionNavigateModel();

            CDPRequest @params = new()
            {
                Method = "Page.getNavigationHistory",
                Params = new NavigateGoToForwardParams
                {
                    TargetId = _debuggerPageResponse.Id
                }
            };

            CDPResult? response = await _cdp.SendInstantAsync(@params);
            ArgumentNullException.ThrowIfNull(response);

            NavigationHistoryResult? navigationHistoryResult = response.Deserialize(JsonContext.Default.NavigationHistoryResult, true);
            ArgumentNullException.ThrowIfNull(navigationHistoryResult);

            int? currentIndex = navigationHistoryResult.CurrentIndex;
            ArgumentNullException.ThrowIfNull(currentIndex);

            List<NavigationHistoryEntriesResult>? entries = navigationHistoryResult.entries;
            ArgumentNullException.ThrowIfNull(entries);

            if (currentIndex <= 0 || currentIndex + 1 > entries.Count) return;

            NavigationHistoryEntriesResult entry = entries[currentIndex.Value + 1];
            ArgumentNullException.ThrowIfNull(entry);

            await GoToUrlAsync(entry.Url!, option);
        }

        public async Task GoToUrlAsync(string url, OptionNavigateModel? option = null)
        {
            option ??= new OptionNavigateModel();

            CDPRequest @params = new()
            {
                Method = "Page.navigate",
                Params = new NavigateGoToUrlParams
                {
                    Url = url,
                    TargetId = _debuggerPageResponse.Id
                }
            };

            CDPResult? response = await _cdp.SendInstantAsync(@params);
            ArgumentNullException.ThrowIfNull(response);

            NavigateGoToUrlFrameResult? navigateGoToUrlFrameResult = response.Deserialize(JsonContext.Default.NavigateGoToUrlFrameResult, true);
            ArgumentNullException.ThrowIfNull(navigateGoToUrlFrameResult);

            if (option.WaitUntilPageLoad)
            {
                Task<bool> isWait = _cdp.WaitMethodAsync("Page.frameStoppedLoading");
                ArgumentNullException.ThrowIfNull(isWait);

                isWait.Wait(new TimeSpan(0, 0, option.Timeout));
            }
        }

        public async Task RefreshAsync(OptionNavigateModel? option = null)
        {
            option ??= new OptionNavigateModel();

            CDPRequest @params = new()
            {
                Method = "Page.reload",
                Params = new NavigateRefreshParams
                {
                    IgnoreCache = option.IgnoreCache,
                    TargetId = _debuggerPageResponse.Id
                }
            };

            await _cdp.SendAsync(@params);

            if (option.WaitUntilPageLoad && !option.IgnoreCache)
            {
                Task<bool> isWait = _cdp.WaitMethodAsync("Page.loadEventFired");
                ArgumentNullException.ThrowIfNull(isWait);

                isWait.Wait(new TimeSpan(0, 0, option.Timeout));
            }
        }
    }
}
