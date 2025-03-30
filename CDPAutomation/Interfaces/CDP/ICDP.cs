using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Interfaces.CDP
{
    public interface ICDP
    {
        Task ConnectAsync(string? webSocket);
        Task DisconnectAsync();

        Task SendAsync(string method, object? parameters = null);
        Task<string?> SendInstant(string method, object? parameters = null);
        Task ListenAsync(string method, Func<object?, Task> callback);
    }
}
