using System.Net.WebSockets;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorSyncTask.Services.Http;

public class WebSocketService : IWebSocketService
{
    private ClientWebSocket webSocket;
    public event Action<string> TaskUpdateReceived;

    public async Task ConnectWebSocket(string url)
    {
        webSocket = new ClientWebSocket();
        await webSocket.ConnectAsync(new Uri(url), CancellationToken.None);

        // Start receiving messages in a background task
        _ = ReceiveMessages();
    }

    public void DisconnectWebSocket()
    {
        webSocket?.Dispose();
        webSocket = null;
    }

    private async Task ReceiveMessages()
    {
        var buffer = new byte[4096];
        while (webSocket.State == WebSocketState.Open)
        {
            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            if (result.MessageType == WebSocketMessageType.Text)
            {
                var message = System.Text.Encoding.UTF8.GetString(buffer, 0, result.Count);
                TaskUpdateReceived?.Invoke(message);
            }
        }
    }
}