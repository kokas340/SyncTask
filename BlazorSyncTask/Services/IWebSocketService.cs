namespace BlazorSyncTask.Services;

public interface IWebSocketService
{
    Task ConnectWebSocket(string url);
    void DisconnectWebSocket();
    event Action<string> TaskUpdateReceived;
}