using CDPAutomation.Abstracts;
using CDPAutomation.Interfaces.CDP;
using CDPAutomation.Interfaces.FindElement;
using CDPAutomation.Models.Browser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Implementation
{
    internal class FindElementImplementation(ICDP cdp, DebuggerPageResponse debuggerPageResponse) : AbstractInitializeImplementation(cdp, debuggerPageResponse), IFindElement
    {
        public Task<IElement?> FindById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IElement?> FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IElement?> FindByClassName(string className)
        {
            throw new NotImplementedException();
        }

        public Task<IElement?> FindByCssSelector(string cssSelector)
        {
            throw new NotImplementedException();
        }

        public Task<IElement?> FindByXPath(string xpath)
        {
            throw new NotImplementedException();
        }

        public Task<List<IElement>> FindsById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<IElement>> FindsByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<List<IElement>> FindsByClassName(string className)
        {
            throw new NotImplementedException();
        }

        public Task<List<IElement>> FindsByCssSelector(string cssSelector)
        {
            throw new NotImplementedException();
        }

        public Task<List<IElement>> FindsByXPath(string xpath)
        {
            throw new NotImplementedException();
        }
    }
}
