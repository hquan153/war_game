using NativeWebSocket;
using UnityEngine;

public class Connection : MonoBehaviour
{
    public WebSocket websocket;

    private void Awake()
    {
        websocket = new WebSocket("ws://127.0.0.1:8080");
    }

    private void Update()
    {
        websocket.DispatchMessageQueue();
    }

    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }

    async public void ReconnectToServer()
    {
        await websocket.Connect();
    }
}