using CDPAutomation.Interfaces.CDP;
using CDPAutomation.Interfaces.Models.Browser;
using CDPAutomation.Interfaces.Request;
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

        Task StartAsync(StartOption? option = null);
        Task CloseAsync();

        Task<IPage> NewPageAsync();
        Task<List<IPage>> Pages();
    }
}
