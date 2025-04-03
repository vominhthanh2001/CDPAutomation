using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Interfaces.JavaScript
{
    public interface IJavaScriptExecutor
    {
        Task<object> ExecuteJavaScriptAsync(string script, bool returnValue = false);
    }
}
