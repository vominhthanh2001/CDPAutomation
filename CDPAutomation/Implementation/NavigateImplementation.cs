using CDPAutomation.Interfaces.Browser;
using CDPAutomation.Interfaces.CDP;
using CDPAutomation.Models.Browser;
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

        public Task GoToBackAsync(OptionNavigate? option = null)
        {
            throw new NotImplementedException();
        }

        public Task GoToForwardAsync(OptionNavigate? option = null)
        {
            throw new NotImplementedException();
        }

        public Task GoToUrlAsync(string url, OptionNavigate? option = null)
        {
            throw new NotImplementedException();
        }

        public Task RefreshAsync(OptionNavigate? option = null)
        {
            throw new NotImplementedException();
        }
    }
}
