using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Interfaces.FindElement
{
    public interface IFindElement
    {
        Task<IElement> FindElement(string selector);
        Task<IElement> FindElementById(string id);
        Task<IElement> FindElementByClassName(string className);
        Task<IElement> FindElementByCssSelector(string cssSelector);
        Task<IElement> FindElementByXPath(string xpath);

        Task<List<IElement>> FindElements(string selector);
        Task<List<IElement>> FindElementsById(string id);
        Task<List<IElement>> FindElementsByClassName(string className);
        Task<List<IElement>> FindElementsByCssSelector(string cssSelector);
        Task<List<IElement>> FindElementsByXPath(string xpath);
    }
}
