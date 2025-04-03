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
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Implementation
{
    internal class PageImplementation(ICDP cdp, DebuggerPageResponse page) : IPage
    {
        private readonly ICDP _cdp = cdp ?? throw new ArgumentNullException(nameof(cdp));
        private readonly ICookie _cookie = default!;
        private readonly INavigate _navigate = default!;
        private readonly IWindow _window = default!;
        private readonly IRequest _request = default!;
        private readonly IJavaScriptExecutor _javascript = new JavaScriptImplementation(cdp, page);
        private readonly IFindElement _findElement = default!;

        public DebuggerPageResponse DebuggerPage { get; private set; } = page ?? throw new ArgumentNullException(nameof(page));

        public Task ActivateAsync()
        {
            CDPRequest @params = new()
            {
                Method = "Target.activateTarget",
                Params = new PageActivateParams
                {
                    TargetId = DebuggerPage?.Id ?? throw new ArgumentNullException(nameof(DebuggerPage.Id))
                }
            };

            _cdp?.SendAsync(@params);

            return Task.CompletedTask;
        }

        public Task CloseAsync()
        {
            CDPRequest @params = new()
            {
                Method = "Target.closeTarget",
                Params = new PageCloseParams
                {
                    TargetId = DebuggerPage?.Id ?? throw new ArgumentNullException(nameof(DebuggerPage.Id))
                }
            };

            Task taskClose = _cdp.SendAsync(@params);
            ArgumentNullException.ThrowIfNull(taskClose);

            _cdp.DisconnectAsync().Wait();

            PageObjectManager.RemovePage(this);

            return Task.CompletedTask;
        }

        public ICookie Cookies() => _cookie;
        public IFindElement FindElement() => _findElement;
        public IJavaScriptExecutor Javascript() => _javascript;
        public INavigate Navigate() => _navigate;
        public IRequest Request() => _request;
        public IWindow Window() => _window;
    }
}
