using CDPAutomation.Interfaces.CDP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Implementation
{
    public class CDPImplementation : ICDP
    {
        public Task ConnectAsync(string? webSocket)
        {
            throw new NotImplementedException();
        }

        public Task DisconnectAsync()
        {
            throw new NotImplementedException();
        }

        public Task ListenAsync(string method, Func<object?, Task> callback)
        {
            throw new NotImplementedException();
        }

        public Task SendAsync(string method, object? parameters = null)
        {
            throw new NotImplementedException();
        }
    }
}
