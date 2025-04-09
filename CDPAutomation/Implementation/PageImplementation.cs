using CDPAutomation.Enums.FindElement;
using CDPAutomation.Extensions;
using CDPAutomation.Helpers;
using CDPAutomation.Interfaces.Browser;
using CDPAutomation.Interfaces.CDP;
using CDPAutomation.Interfaces.FindElement;
using CDPAutomation.Interfaces.JavaScript;
using CDPAutomation.Interfaces.Pages;
using CDPAutomation.Interfaces.Request;
using CDPAutomation.Models.Browser;
using CDPAutomation.Models.CDP;
using CDPAutomation.Models.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Implementation
{
    internal class PageImplementation : IPage
    {
        private readonly ICDP _cdp;
        private readonly ICookie _cookie = default!;
        private readonly INavigate _navigate = default!;
        private readonly IWindow _window = default!;
        private readonly IRequest _request = default!;
        private readonly IJavaScriptExecutor _javascript = default!;
        private readonly IFindElement _findElementCoreJavaScript = default!;

        public PageImplementation(ICDP cdp, DebuggerPageResult page)
        {
            _cdp = cdp ?? throw new ArgumentNullException(nameof(cdp));
            _cdp.SendAsync(new CDPRequest { Method = "Page.enable" });
            //_cdp.SendAsync(new CDPRequest { Method = "Network.enable" });
            //_cdp.SendAsync(new CDPRequest { Method = "DOM.enable" });
            //_cdp.SendAsync(new CDPRequest { Method = "Runtime.enable" });
            _cdp.SendAsync(new CDPRequest { Method = "Page.setLifecycleEventsEnabled", Params = new SetLifecycleEventsEnabledParams { Enabled = true } });

            _javascript = new JavaScriptImplementation(cdp, page);
            _navigate = new NavigateImplementation(cdp, page);
            _findElementCoreJavaScript = new FindElementImplementation(cdp, page);

            DebuggerPage = page ?? throw new ArgumentNullException(nameof(page));
        }

        public DebuggerPageResult DebuggerPage { get; private set; }

        public async Task ActivateAsync()
        {
            CDPRequest @params = new()
            {
                Method = "Target.activateTarget",
                Params = new PageActivateParams
                {
                    TargetId = DebuggerPage?.Id ?? throw new ArgumentNullException(nameof(DebuggerPage.Id))
                }
            };

            await _cdp.SendAsync(@params);
        }

        public async Task CloseAsync()
        {
            CDPRequest @params = new()
            {
                Method = "Target.closeTarget",
                Params = new PageCloseParams
                {
                    TargetId = DebuggerPage?.Id ?? throw new ArgumentNullException(nameof(DebuggerPage.Id))
                }
            };

            await _cdp.SendAsync(@params);

            await _cdp.DisconnectAsync();

            PageObjectManager.RemovePage(this);
        }

        public ICookie Cookies() => _cookie;
        public IFindElement FindElement() => _findElementCoreJavaScript;
        public IJavaScriptExecutor Javascript() => _javascript;
        public INavigate Navigate() => _navigate;
        public IRequest Request() => _request;
        public IWindow Window() => _window;
    }
}
