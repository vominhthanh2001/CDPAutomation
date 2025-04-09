using CDPAutomation.Interfaces.CDP;
using CDPAutomation.Models.Browser;
using CDPAutomation.Models.FindElement.Element;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Abstracts
{
    internal abstract class AbstractInitializeImplementation
    {
        protected AbstractInitializeImplementation() { }
        
        protected AbstractInitializeImplementation(DebuggerPageResult debuggerPageResponse)
        {
            _ = debuggerPageResponse;
        }

        protected AbstractInitializeImplementation(ICDP cdp, DebuggerPageResult debuggerPageResponse)
        {
            _ = cdp;
            _ = debuggerPageResponse;
        }
    }
}
