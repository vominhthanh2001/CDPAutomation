using CDPAutomation.Interfaces.Browser;
using CDPAutomation.Interfaces.FindElement;
using CDPAutomation.Interfaces.JavaScript;
using CDPAutomation.Interfaces.Request;
using CDPAutomation.Models.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Interfaces.Pages
{
    public interface IPage
    {
        INavigate Navigate();
        IWindow Window();
        ICookie Cookies();
        IRequest Request();
        IJavaScriptExecutor Javascript();
        IFindElement FindElement();
        
        Task ActivateAsync();
        Task CloseAsync();
    }
}
