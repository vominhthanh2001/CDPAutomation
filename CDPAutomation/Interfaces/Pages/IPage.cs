using CDPAutomation.Interfaces.Browser;
using CDPAutomation.Interfaces.FindElement;
using CDPAutomation.Interfaces.JavaScript;
using CDPAutomation.Interfaces.Request;

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
