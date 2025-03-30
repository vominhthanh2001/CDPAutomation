using CDPAutomation.Implementation;
using CDPAutomation.Interfaces.Browser;
using CDPAutomation.Interfaces.CDP;
using CDPAutomation.Interfaces.JavaScript;
using CDPAutomation.Interfaces.Request;
using CDPAutomation.Models.Browser;
using CDPAutomation.Pages;

namespace CDPAutomation.Drivers
{
    public class ChromeBrowser : IBrowser, IDisposable
    {
        public ICDP CDP { get; }
        public INavigate Navigate { get; }
        public ICookie Cookies { get; }
        public IRequest Request { get; }
        public IJavaScriptExecutor Javascript { get; }

        public ChromeBrowser()
        {
            this.CDP = new CDPImplementation();
            this.Navigate = new NavigateImplementation();
            this.Cookies = new CookieImplementation();
            this.Request = new RequestImplementation();
            this.Javascript = new JavaScriptExecutorImplementation();
        }

        public Task StartAsync(StartOption? option = null)
        {
            throw new NotImplementedException();
        }

        public Task CloseAsync()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<IPage> NewPageAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<IPage>> Pages()
        {
            throw new NotImplementedException();
        }
    }
}
