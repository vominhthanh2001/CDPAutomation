using CDPAutomation.Interfaces.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Interfaces.FindElement
{
    public interface IFindElement
    {
        Task<IElement?> ById(string id);
        Task<IElement?> ByName(string name);
        Task<IElement?> ByClassName(string className);
        Task<IElement?> ByCssSelector(string cssSelector);
        Task<IElement?> ByXPath(string xpath);

        Task<List<IElement>> BysId(string id);
        Task<List<IElement>> BysName(string name);
        Task<List<IElement>> BysClassName(string className);
        Task<List<IElement>> BysCssSelector(string cssSelector);
        Task<List<IElement>> BysXPath(string xpath);
    }
}
