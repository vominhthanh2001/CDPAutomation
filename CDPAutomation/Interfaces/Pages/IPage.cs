using CDPAutomation.Enums.FindElement;
using CDPAutomation.Interfaces.Browser;
using CDPAutomation.Interfaces.FindElement;
using CDPAutomation.Interfaces.JavaScript;
using CDPAutomation.Interfaces.Request;
using CDPAutomation.Models.Browser;

namespace CDPAutomation.Interfaces.Pages
{
    public interface IPage
    {
        DebuggerPageResult DebuggerPage { get; }

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
