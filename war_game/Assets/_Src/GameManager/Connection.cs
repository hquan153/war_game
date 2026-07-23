using System;
using NativeWebSocket;
using UnityEngine;

public class Connection : MonoBehaviour
{
    private const string serverUrl = "ws://127.0.0.1:8080";

    private WebSocket websocket;

    private GameManager gameManagerScript;

    private void Awake()
    {
        websocket = new WebSocket(serverUrl);

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
            if (bytes == null || bytes.Length < 4) return;

            int playerDataLength = BitConverter.ToInt32(bytes, 0);

            //Debug.Log("bytes: " + bytes.Length);
            //Debug.Log("playerDataLength: " + playerDataLength);

            string playerDataJSON = System.Text.Encoding.UTF8.GetString(bytes, 4, playerDataLength);
            PlayerData player = JsonUtility.FromJson<PlayerData>(playerDataJSON);

            if (player.isWelcome)
            {
                Debug.Log(player.message);
                return;
            }

            int avatarLength = bytes.Length - 4 - playerDataLength;
            if (avatarLength > 0)
            {
                player.avatarBuffer = new byte[avatarLength];
                Array.Copy(bytes, 4 + playerDataLength, player.avatarBuffer, 0, avatarLength);

                gameManagerScript.PlayerDataReceiverHandler(player);
            }
            else Debug.LogError("?");
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