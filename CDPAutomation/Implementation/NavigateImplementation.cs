using CDPAutomation.Helpers;
using CDPAutomation.Interfaces.Browser;
using CDPAutomation.Interfaces.CDP;
using CDPAutomation.Models.Browser;
using CDPAutomation.Models.Navigate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Implementation
{
    public class NavigateImplementation : INavigate
    {
        private readonly ICDP _cdp;
        public NavigateImplementation(ICDP cdp)
        {
            _cdp = cdp;
        }

        public async Task GoToBackAsync(OptionNavigate? option = null)
        {
            string? canGoBack = await _cdp.SendInstantAsync(
                method: "Page.getNavigationHistory");

            if (canGoBack is null) return;

            NavigationHistoryResponse? navigationHistoryResponse = JsonHelper.Deserialize(canGoBack, JsonContext.Default.NavigationHistoryResponse);
            if (navigationHistoryResponse is null || navigationHistoryResponse?.Result is null) return;

            NavigationHistoryResult result = navigationHistoryResponse.Result;
            int currentIndex = result.CurrentIndex;
            if (currentIndex < 0) return;

            var entries = result.Entries;
            if (entries is null) return;
            if (currentIndex - 1 < 0) return;

            string? url = entries?[currentIndex - 1].Url;
            if (url is null) return;

            await GoToUrlAsync(url, option);
        }

        public async Task GoToForwardAsync(OptionNavigate? option = null)
        {
            string? canGoBack = await _cdp.SendInstantAsync(
                method: "Page.getNavigationHistory");

            if (canGoBack is null) return;

            NavigationHistoryResponse? navigationHistoryResponse = JsonHelper.Deserialize(canGoBack, JsonContext.Default.NavigationHistoryResponse);
            if (navigationHistoryResponse is null || navigationHistoryResponse?.Result is null) return;

            NavigationHistoryResult result = navigationHistoryResponse.Result;
            int currentIndex = result.CurrentIndex;
            if (currentIndex < 0) return;

            var entries = result.Entries;
            if (entries is null) return;
            if (currentIndex + 1 >= entries.Count) return;

            string? url = entries?[currentIndex + 1].Url;
            if (url is null) return;

            await GoToUrlAsync(url, option);
        }

        public async Task GoToUrlAsync(string url, OptionNavigate? option = null)
        {
            await _cdp.SendInstantAsync(
                method: "Page.navigate",
                parameters: new NavigateGotoUrl { Url = url });

            if (option is null) return;
            if (option.WaitUntilPageLoad) await _cdp.WaitMethodAsync("Page.frameStoppedLoading", option.Timeout);
        }

        public async Task RefreshAsync(OptionNavigate? option = null)
        {
            await _cdp.SendAsync(
                method: "Page.reload");

            if (option is null) return;
            if (option.WaitUntilPageLoad) await _cdp.WaitMethodAsync("Page.frameStoppedLoading", option.Timeout);
        }
    }
}
