using CDPAutomation.Models.CDP;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Interfaces.CDP
{
    public interface ICDP
    {
        ConcurrentDictionary<int, TaskCompletionSource<string>> ResponseTasks { get; set; }

        Task ConnectAsync(string? webSocket);
        Task DisconnectAsync();

        Task SendAsync(string method, object? parameters = null);
        Task<CDPResponse?> SendInstantAsync(string method, object? parameters = null);
        Task<bool> WaitMethodAsync(string method, int? timeout);

        Task<TaskCompletionSource<string>> GetTaskCompletionSourceAsync(int id);
    }
}
