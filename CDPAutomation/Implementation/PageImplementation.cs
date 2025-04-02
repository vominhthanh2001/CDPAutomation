using CDPAutomation.Interfaces.Browser;
using CDPAutomation.Interfaces.CDP;
using CDPAutomation.Interfaces.FindElement;
using CDPAutomation.Interfaces.JavaScript;
using CDPAutomation.Interfaces.Pages;
using CDPAutomation.Interfaces.Request;
using CDPAutomation.Models.Page;
using System.Threading.Tasks;

namespace CDPAutomation.Implementation
{
    public class PageImplementation : IPage
    {
        private readonly ICDP _cdp;
        private readonly TargetInfo _targetInfo;
        public PageImplementation(ICDP cdp, TargetInfo targetInfo)
        {
            _cdp = cdp;
            _targetInfo = targetInfo;

            _cdp.SendAsync(method: "Page.enable");
            _cdp.SendAsync(method: "Network.enable");
        }

        public async Task ActivateAsync()
        {
            await _cdp.SendAsync(
                method: "Target.activateTarget",
                parameters: new ActivateTargetParams { TargetId = this._targetInfo.TargetId }
            );
        }

        public async Task CloseAsync()
        {
            await _cdp.SendAsync(
                method: "Target.closeTarget",
                parameters: new CloseTargetParams { TargetId = this._targetInfo.TargetId }
            );
        }

        public INavigate Navigate() => new NavigateImplementation(_cdp);
        public IWindow Window() => new WindowImplementation(_cdp);
        public ICookie Cookies() => new CookieImplementation(_cdp, _targetInfo);
        public IRequest Request() => throw new NotImplementedException();
        public IJavaScriptExecutor Javascript() => new JavaScriptExecutorImplementation(_cdp);
        public IFindElement FindElement() => new FindElementImplementation(_cdp);
    }
}
