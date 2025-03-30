using CDPAutomation.Interfaces.Browser;
using CDPAutomation.Interfaces.CDP;
using CDPAutomation.Interfaces.Fingerprint;
using CDPAutomation.Interfaces.JavaScript;
using CDPAutomation.Interfaces.Request;
using CDPAutomation.Models.Browser;
using CDPAutomation.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Drivers
{
    public class FirefoxBrowser : IBrowser, IDisposable
    {
        public ICDP CDP => throw new NotImplementedException();

        public INavigate Navigate => throw new NotImplementedException();

        public IWindow Window => throw new NotImplementedException();

        public ICookie Cookies => throw new NotImplementedException();

        public IRequest Request => throw new NotImplementedException();

        public IJavaScriptExecutor Javascript => throw new NotImplementedException();

        public IFingerprint Fingerprint => throw new NotImplementedException();

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

        public Task StartAsync(StartOption? option = null)
        {
            throw new NotImplementedException();
        }
    }
}
