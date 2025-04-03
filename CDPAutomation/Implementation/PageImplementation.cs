using CDPAutomation.Interfaces.Browser;
using CDPAutomation.Interfaces.FindElement;
using CDPAutomation.Interfaces.JavaScript;
using CDPAutomation.Interfaces.Pages;
using CDPAutomation.Interfaces.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Implementation
{
    public class PageImplementation : IPage
    {
        public Task ActivateAsync()
        {
            throw new NotImplementedException();
        }

        public Task CloseAsync()
        {
            throw new NotImplementedException();
        }

        public ICookie Cookies()
        {
            throw new NotImplementedException();
        }

        public IFindElement FindElement()
        {
            throw new NotImplementedException();
        }

        public IJavaScriptExecutor Javascript()
        {
            throw new NotImplementedException();
        }

        public INavigate Navigate()
        {
            throw new NotImplementedException();
        }

        public IRequest Request()
        {
            throw new NotImplementedException();
        }

        public IWindow Window()
        {
            throw new NotImplementedException();
        }
    }
}
