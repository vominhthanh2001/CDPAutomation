using CDPAutomation.Abstracts;
using CDPAutomation.Helpers;
using CDPAutomation.Interfaces.Events;
using CDPAutomation.Models.Browser;
using System.Collections.Concurrent;
using System.Text.Json;

namespace CDPAutomation.Implementation.Events
{
    public class CDPEventImplementation : CDPEvents
    {
        protected ConcurrentDictionary<string, TaskCompletionSource<bool>> _waitingEvents = new();
        protected ConcurrentDictionary<int, TaskCompletionSource<string>> _responseTasks = new();

        public event EventHandler<string>? OnMessageReceivedResponse;
        protected virtual void OnMessageReceivedResponseEvent(string e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(e)) return;

                Console.WriteLine(e);
                string? id = JsonHelper.GetProperty(e, "id");
                if (id is null) return;

                if (_responseTasks.TryRemove(int.Parse(id), out TaskCompletionSource<string>? task))
                {
                    if (task is null) return;

                    task.SetResult(e);
                }
            }
            catch (Exception) { }

            OnMessageReceivedResponse?.Invoke(this, e);
        }

        public event EventHandler<string>? OnMessageReceivedWaitMethod;
        protected virtual void OnMessageReceivedWaitMethodEvent(string e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(e)) return;

                var data = JsonDocument.Parse(e);
                if (data.RootElement.TryGetProperty("method", out var methodElement))
                {
                    string? method = methodElement.GetString();
                    if (method != null && _waitingEvents.TryGetValue(method, out var tcs))
                    {
                        tcs.TrySetResult(true);
                    }
                }
            }
            catch (Exception) { }

            OnMessageReceivedWaitMethod?.Invoke(this, e);
        }
    }
}
