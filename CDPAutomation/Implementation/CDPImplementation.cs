using CDPAutomation.Helpers;
using CDPAutomation.Implementation.Events;
using CDPAutomation.Interfaces.CDP;
using CDPAutomation.Models.CDP;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CDPAutomation.Implementation
{
    internal class CDPImplementation : CDPEvent, ICDP
    {
        private readonly ClientWebSocket _clientWebSocket = new();

        public ConcurrentDictionary<int, TaskCompletionSource<string>> ResponseTasks
        {
            get { return _responseTasks; }
            set { _responseTasks = value; }
        }

        public Task<bool> IsConnect()
        {
            if (_clientWebSocket is null) return Task.FromResult(false);
            if (_clientWebSocket.State != WebSocketState.Open) return Task.FromResult(false);

            return Task.FromResult(true);
        }

        public async Task ConnectAsync(string? webSocket)
        {
            if (string.IsNullOrWhiteSpace(webSocket)) throw new ArgumentNullException(nameof(webSocket));

            await _clientWebSocket.ConnectAsync(new Uri(webSocket), CancellationToken.None);

            _ = await Task.Factory.StartNew(async () =>
            {
                while (_clientWebSocket.State == WebSocketState.Open)
                {
                    var buffer = new byte[8192];
                    var resultBuffer = new ArraySegment<byte>(buffer);
                    using var ms = new MemoryStream();

                    WebSocketReceiveResult result;

                    do
                    {
                        result = await _clientWebSocket.ReceiveAsync(resultBuffer, CancellationToken.None);
                        ms.Write(buffer, 0, result.Count);
                    }
                    while (!result.EndOfMessage);

                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await _clientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                    }
                    else
                    {
                        string messageText = Encoding.UTF8.GetString(buffer, 0, result.Count);

                        string? id = JsonHelper.GetProperty(messageText, "id");
                        if (!string.IsNullOrWhiteSpace(id))
                            Console.WriteLine($"[RESPONSE EVENT] {messageText}\r\n");

                        OnMessageReceivedResponseEvent(messageText);
                        OnMessageReceivedWaitMethodEvent(messageText);
                    }
                }
            });
        }

        public async Task DisconnectAsync()
        {
            if (_clientWebSocket is null) return;

            await _clientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Disconnect", CancellationToken.None);
            _clientWebSocket.Dispose();
        }

        public async Task SendAsync(CDPRequest? data)
        {
            ArgumentNullException.ThrowIfNull(data);
            ArgumentNullException.ThrowIfNull(_clientWebSocket);

            if (!await IsConnect())
                throw new Exception("Not connected to the browser");

            string? json = JsonHelper.Serialize(data);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(json);

            var bytesToSend = Encoding.UTF8.GetBytes(json);
            var sendBuffer = new ArraySegment<byte>(bytesToSend);

            await _clientWebSocket.SendAsync(sendBuffer, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public async Task<CDPResponse?> SendInstantAsync(CDPRequest? data)
        {
            ArgumentNullException.ThrowIfNull(data);
            ArgumentNullException.ThrowIfNull(_clientWebSocket);

            if (!await IsConnect())
                throw new Exception("Not connected to the browser");

            var taskReceivedCompletionSource = new TaskCompletionSource<string>();
            _responseTasks[data.Id] = taskReceivedCompletionSource;

            string? json = JsonHelper.Serialize(data);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(json);

            var bytesToSend = Encoding.UTF8.GetBytes(json);
            var sendBuffer = new ArraySegment<byte>(bytesToSend);
            await _clientWebSocket.SendAsync(sendBuffer, WebSocketMessageType.Text, true, CancellationToken.None);

            string taskReceivedCompletionSourceResult = await taskReceivedCompletionSource.Task;
            string? result = JsonHelper.GetProperty(taskReceivedCompletionSourceResult, "result");
            ArgumentNullException.ThrowIfNullOrWhiteSpace(result);

            return new()
            {
                Id = data.Id,
                Result = JsonHelper.Deserialize(result, JsonContext.Default.JsonElement),
                Error = null
            };
        }

        public Task<bool> WaitMethodAsync(string? method)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(method);

            TaskCompletionSource<bool> taskCompletionSource = new();
            _waitingEvents[method] = taskCompletionSource;

            Task? taskWaitMethod = taskCompletionSource.Task;
            ArgumentNullException.ThrowIfNull(taskWaitMethod);

            return taskWaitMethod.ContinueWith(t =>
            {
                if (t.IsCompletedSuccessfully)
                {
                    _waitingEvents.TryRemove(method, out _);
                    return true;
                }
                else
                {
                    _waitingEvents.TryRemove(method, out _);
                    return false;
                }
            });
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
