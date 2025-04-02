using CDPAutomation.Interfaces.CDP;
using CDPAutomation.Interfaces.Fingerprint;
using CDPAutomation.Interfaces.JavaScript;
using CDPAutomation.Interfaces.Pages;
using CDPAutomation.Interfaces.Request;
using CDPAutomation.Models.Browser;
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
        IFingerprint Fingerprint { get; }
        IPage? PageTarget { get; }

        Task StartAsync(StartOption? option = null);
        Task CloseAsync();

        Task<List<IPage>> Pages();
        Task<IPage> NewPageAsync();
        Task SwitchPage(IPage? page);
        Task SwitchPage(int? index);
    }
}
