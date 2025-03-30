using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Interfaces.JavaScript
{
    public interface IJavaScriptExecutor
    {
        Task ExecuteJavaScriptAsync(string script);
        Task<T> ExecuteJavaScriptAsync<T>(string script);
    }
}
