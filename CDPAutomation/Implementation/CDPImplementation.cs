using CDPAutomation.Helpers;
using CDPAutomation.Implementation.Events;
using CDPAutomation.Interfaces.CDP;
using CDPAutomation.Models.CDP;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Websocket.Client;

namespace CDPAutomation.Implementation
{
    public class CDPImplementation : CDPEventImplementation, ICDP
    {
        public ConcurrentDictionary<int, TaskCompletionSource<string>> ResponseTasks
        {
            get { return _responseTasks; }
            set { _responseTasks = value; }
        }

        private WebsocketClient? _websocketClient;

        public async Task ConnectAsync(string? webSocket)
        {
            if (string.IsNullOrWhiteSpace(webSocket)) throw new ArgumentNullException(nameof(webSocket));
            _websocketClient = new(new Uri(webSocket));
            await _websocketClient.Start();

            _ = _websocketClient.MessageReceived.Subscribe(message => OnMessageReceivedResponseEvent(message?.Text ?? string.Empty));
            _ = _websocketClient.MessageReceived.Subscribe(message => OnMessageReceivedWaitMethodEvent(message?.Text ?? string.Empty));
        }

        public async Task DisconnectAsync()
        {
            if (_websocketClient is null) return;

            await _websocketClient.Stop(WebSocketCloseStatus.NormalClosure, "Disconnect");
        }

        public Task SendAsync(string method, object? parameters = null)
        {
            if (_websocketClient is null) throw new Exception("WebSocketClient is null");

            var message = new CDPRequest
            {
                Id = CDPHelper.GetMessageId(),
                Method = method,
                Params = parameters
            };

            string? json = JsonHelper.Serialize(message);
            if (json is null) return Task.CompletedTask;

            _websocketClient.Send(json);
            return Task.CompletedTask;
        }

        public async Task<CDPResponse?> SendInstantAsync(string method, object? parameters = null)
        {
            if (_websocketClient is null) throw new Exception("WebSocketClient is null");

            var message = new CDPRequest
            {
                Id = CDPHelper.GetMessageId(),
                Method = method,
                Params = parameters
            };

            var taskCompletionSource = new TaskCompletionSource<string>();
            _responseTasks[message.Id] = taskCompletionSource;

            string? json = JsonHelper.Serialize(message);
            if (json is null) return default;

            await _websocketClient.SendInstant(json);
            string resultTask = await taskCompletionSource.Task;

            CDPResponse? response = JsonHelper.Deserialize(resultTask, JsonContext.Default.CDPResponse);
            return response;
        }

        public async Task<bool> WaitMethodAsync(string method, int? timeout)
        {
            if (method == null) throw new ArgumentNullException(nameof(method));
            if (timeout is null) timeout = 60;

            var tcs = new TaskCompletionSource<bool>();
            _waitingEvents[method] = tcs;

            // Timeout nếu sự kiện không xảy ra trong khoảng thời gian giới hạn
            var timeoutTask = Task.Delay(timeout.Value * 1000);
            var completedTask = await Task.WhenAny(tcs.Task, timeoutTask);

            _waitingEvents.TryRemove(method, out _);

            return completedTask == tcs.Task;

        }

        public Task<TaskCompletionSource<string>> GetTaskCompletionSourceAsync(int id)
        {
            if (_responseTasks.TryGetValue(id, out TaskCompletionSource<string>? task))
            {
                return Task.FromResult(task);
            }

            return Task.FromResult(new TaskCompletionSource<string>());
        }
    }
}
