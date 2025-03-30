using CDPAutomation.Interfaces.Browser;
using CDPAutomation.Interfaces.CDP;
using CDPAutomation.Interfaces.Models.Browser;
using CDPAutomation.Interfaces.Request;
using CDPAutomation.Pages;

namespace CDPAutomation.Drivers
{
    public class ChromeBrowser : IBrowser, IDisposable
    {
        public ICDP CDP => throw new NotImplementedException();
        public INavigate Navigate => throw new NotImplementedException();

        public ICookie Cookies => throw new NotImplementedException();

        public IRequest Request => throw new NotImplementedException();

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
