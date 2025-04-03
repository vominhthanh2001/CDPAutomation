using CDPAutomation.Interfaces.CDP;
using CDPAutomation.Interfaces.Fingerprint;
using CDPAutomation.Interfaces.JavaScript;
using CDPAutomation.Interfaces.Pages;
using CDPAutomation.Interfaces.Request;
using CDPAutomation.Models.Browser;
using CDPAutomation.Models.Page;
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
        IPage PageTarget { get; set; }

        Task StartAsync(StartOptionModel? option = null);
        Task CloseAsync();

        Task<List<IPage>> Pages();
        Task<IPage> NewPageAsync();
        Task SwitchPage(IPage? page);
        Task SwitchPage(int? index);
    }
}
