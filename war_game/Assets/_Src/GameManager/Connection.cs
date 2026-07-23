using NativeWebSocket;
using UnityEngine;

public class Connection : MonoBehaviour
{
    private WebSocket websocket;

    private GameManager gameManagerScript;

    private void Awake()
    {
        websocket = new WebSocket("ws://127.0.0.1:8080");

        gameManagerScript = transform.GetComponent<GameManager>();
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
            var interactionDataJSON = System.Text.Encoding.UTF8.GetString(bytes);
            PlayerData interactionData = JsonUtility.FromJson<PlayerData>(interactionDataJSON);
            Debug.Log("Received: " + interactionData.avatarBase64);

            if (interactionData.isWelcome) Debug.Log(interactionData.message);
            else gameManagerScript.InteractionDataReceiverHandler(interactionData);
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