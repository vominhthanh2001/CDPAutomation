using CDPAutomation.Interfaces.CDP;
using CDPAutomation.Interfaces.JavaScript;
using CDPAutomation.Interfaces.Request;
using CDPAutomation.Models.Browser;
using CDPAutomation.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Interfaces.Browser
{
    public interface IBrowser
    {
        ICDP CDP { get; }
        INavigate Navigate { get; }
        ICookie Cookies { get; }
        IRequest Request { get; }
        IJavaScriptExecutor Javascript { get; }

        Task StartAsync(StartOption? option = null);
        Task CloseAsync();

        Task<IPage> NewPageAsync();
        Task<List<IPage>> Pages();
    }
}
