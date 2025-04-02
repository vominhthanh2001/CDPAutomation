using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Interfaces.FindElement
{
    public interface IFindElement
    {
        Task<IElement?> FindById(string id);
        Task<IElement?> FindByClassName(string className);
        Task<IElement?> FindByCssSelector(string cssSelector);
        Task<IElement?> FindByXPath(string xpath);

        Task<List<IElement>> Finds(string selector);
        Task<List<IElement>> FindsById(string id);
        Task<List<IElement>> FindsByClassName(string className);
        Task<List<IElement>> FindsByCssSelector(string cssSelector);
        Task<List<IElement>> FindsByXPath(string xpath);
    }
}
