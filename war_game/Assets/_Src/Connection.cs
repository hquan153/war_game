using NativeWebSocket;
using UnityEngine;

public class Connection : MonoBehaviour
{
    private WebSocket websocket;

    private void Awake()
    {
        Application.runInBackground = true;
        #if !UNITY_EDITOR
            Screen.SetResolution(540, 960, false); 
        #endif

        websocket = new WebSocket("ws://127.0.0.1:8080");
    }

    async private void Start()
    {
        websocket.OnOpen += () =>
        {
            Debug.Log("Connection open!");
        };

        websocket.OnError += (e) => Debug.Log("Error! " + e);

        websocket.OnClose += (code) =>
        {
            Debug.Log("Connection closed!");
        };

        websocket.OnMessage += (bytes) =>
        {
            var message = System.Text.Encoding.UTF8.GetString(bytes);
            //Debug.Log("Received: " + message);
        };

        await websocket.Connect();
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