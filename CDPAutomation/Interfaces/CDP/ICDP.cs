using CDPAutomation.Interfaces.Events;
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
        
        Task<bool> IsConnect();
        Task ConnectAsync(string? webSocket);
        Task DisconnectAsync();

        Task SendAsync(CDPRequest? data);
        Task<CDPResponse?> SendInstantAsync(CDPRequest? data);
        Task<bool> WaitMethodAsync(string? method);

        Task<TaskCompletionSource<string>> GetTaskCompletionSourceAsync(int id);
    }
}
