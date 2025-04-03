using CDPAutomation.Helpers;
using CDPAutomation.Implementation.Events;
using CDPAutomation.Interfaces.CDP;
using CDPAutomation.Models.Browser;
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
    internal class CDPImplementation : CDPEventImplementation, ICDP
    {
        public ConcurrentDictionary<int, TaskCompletionSource<string>> ResponseTasks
        {
            get { return _responseTasks; }
            set { _responseTasks = value; }
        }

        private WebsocketClient? _websocketClient;

        public Task<bool> IsConnect()
        {
            if (_websocketClient is null) return Task.FromResult(false);
            if (_websocketClient.IsRunning) return Task.FromResult(true);
            return Task.FromResult(false);
        }

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

        public async Task SendAsync(CDPRequest? data)
        {
            ArgumentNullException.ThrowIfNull(data);

            if (!await IsConnect())
                throw new Exception("Not connected to the browser");

            string? json = JsonHelper.Serialize(data);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(json);

            _websocketClient?.Send(json);
        }

        public async Task<CDPResponse?> SendInstantAsync(CDPRequest? data)
        {
            ArgumentNullException.ThrowIfNull(data);

            if (!await IsConnect())
                throw new Exception("Not connected to the browser");

            string? json = JsonHelper.Serialize(data);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(json);

            TaskCompletionSource<string> taskCompletionSource = new();
            ResponseTasks[data.Id] = taskCompletionSource;

            Task? taskSendInstant = _websocketClient?.SendInstant(json);
            ArgumentNullException.ThrowIfNull(taskSendInstant);
            await taskSendInstant;

            string resultTask = await taskCompletionSource.Task;
            CDPResponse? cdpResponse = JsonHelper.Deserialize(resultTask, JsonContext.Default.CDPResponse);
            ArgumentNullException.ThrowIfNull(cdpResponse);

            return cdpResponse;
        }

        public Task<bool> WaitMethodAsync(object? data)
        {
            throw new NotImplementedException();
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
