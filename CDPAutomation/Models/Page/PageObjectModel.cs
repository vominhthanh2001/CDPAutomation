using CDPAutomation.Interfaces.Pages;
using CDPAutomation.Models.Browser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Models.Page
{
    public class PageObjectModel
    {
        public int Index { get; set; } = 0;
        public IPage Page { get; set; } = default!;
        public DebuggerPageResponse DebuggerPage { get; set; } = default!;
        public int Port { get; set; } 
        public string IdPage => DebuggerPage?.Id ?? string.Empty;
        public bool Active { get; set; } = false;

        public string WebSocket
        {
            get => $"ws://localhost:{Port}/devtools/page/{IdPage}";
        }
    }
}
