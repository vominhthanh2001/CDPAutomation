using CDPAutomation.Interfaces.CDP;
using CDPAutomation.Interfaces.FindElement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Implementation
{
    public class FindElementImplementation : IFindElement
    {
        private readonly ICDP _cdp;
        public FindElementImplementation(ICDP cdp)
        {
            _cdp = cdp;
        }

        public Task<IElement?> FindByClassName(string className)
        {
            throw new NotImplementedException();
        }

        public Task<IElement?> FindByCssSelector(string cssSelector)
        {
            throw new NotImplementedException();
        }

        public Task<IElement?> FindById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IElement?> FindByXPath(string xpath)
        {
            throw new NotImplementedException();
        }

        public Task<List<IElement>> Finds(string selector)
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

        public Task<List<IElement>> FindsById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<IElement>> FindsByXPath(string xpath)
        {
            throw new NotImplementedException();
        }
    }
}
