using CDPAutomation.Helpers;
using CDPAutomation.Interfaces.CDP;
using CDPAutomation.Models.CDP;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Websocket.Client;

namespace CDPAutomation.Implementation
{
    public class CDPImplementation : ICDP
    {
        internal static ConcurrentDictionary<int, TaskCompletionSource<string>> _responseTasks = new();

        private WebsocketClient? _websocketClient;

        public async Task ConnectAsync(string? webSocket)
        {
            if (string.IsNullOrWhiteSpace(webSocket)) throw new ArgumentNullException(nameof(webSocket));
            _websocketClient = new(new Uri(webSocket));
            await _websocketClient.Start();
        }

        public async Task DisconnectAsync()
        {
            if (_websocketClient is null) return;

            await _websocketClient.Stop(WebSocketCloseStatus.NormalClosure, "Disconnect");
        }

        public Task ListenAsync(string method, Func<object?, Task> callback)
        {
            if (_websocketClient is null) throw new Exception("WebSocketClient is null");

            _ =_websocketClient.MessageReceived.Subscribe(async message =>
            {
                string? json = message.Text;
                if (json is null) return;
                string? methodName = JsonHelper.GetProperty(json, "method");
                if (methodName is null) return;
                if (methodName == method)
                {
                    object? parameters = JsonHelper.GetProperty(json, "params");
                    await callback(parameters);
                }

                // Lấy id của message để trả về kết quả cho message
                string? id = JsonHelper.GetProperty(json, "id");
                if (id is null) return;

                if (_responseTasks.TryRemove(int.Parse(id), out TaskCompletionSource<string>? task))
                {
                    if (task is null) return;

                    task.SetResult(json);
                }

            });
            return Task.CompletedTask;

        }

        public Task SendAsync(string method, object? parameters = null)
        {
            if (_websocketClient is null) throw new Exception("WebSocketClient is null");
            if (parameters is null) return Task.CompletedTask;

            var message = new
            {
                id = CDPHelper.GetMessageId(),
                method = method,
                @params = parameters
            };

            string? json = JsonHelper.Serialize(message);
            if (json is null) return Task.CompletedTask;

            _websocketClient.Send(json);
            return Task.CompletedTask;
        }

        public async Task<string?> SendInstant(string method, object? parameters = null)
        {
            if (_websocketClient is null) throw new Exception("WebSocketClient is null");
            if (parameters is null) return default;

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

            return resultTask;
        }
    }
}
