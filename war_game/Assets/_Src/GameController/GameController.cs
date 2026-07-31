using System;
using NativeWebSocket;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private const string serverUrl = "ws://127.0.0.1:8080";

    private WebSocket websocket;

    private PlayerController playerControllerScript;

    private GameObject disconnectedUI;

    private void Awake()
    {
        websocket = new WebSocket(serverUrl);

        playerControllerScript = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerController>();
        disconnectedUI = GameObject.FindGameObjectWithTag("Disconnected");
    }

    private async void Start()
    {
        websocket.OnOpen += () =>
        {
            Debug.Log("Connection open!");
            disconnectedUI.SetActive(false);
        };

        websocket.OnError += (e) => Debug.Log("Error! " + e);

        websocket.OnClose += (code) =>
        {
            Debug.Log("Connection closed!");
            disconnectedUI.SetActive(true);
        };

        websocket.OnMessage += (bytes) =>
        {
            if (bytes == null || bytes.Length < 4) return;

            int playerDataLength = BitConverter.ToInt32(bytes, 0);

            string playerDataJSON = System.Text.Encoding.UTF8.GetString(bytes, 4, playerDataLength);
            PlayerData player = JsonUtility.FromJson<PlayerData>(playerDataJSON);

            int avatarLength = bytes.Length - 4 - playerDataLength;
            if (avatarLength > 0)
            {
                player.avatarBuffer = new byte[avatarLength];
                Array.Copy(bytes, 4 + playerDataLength, player.avatarBuffer, 0, avatarLength);
                //Debug.Log("received the interaction data!");
                playerControllerScript.Players = player;
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

    public async void Reconnect()
    {
        await websocket.Connect();
    }

    public async void RemovePlayer(string displayId)
    {
        if (websocket.State == WebSocketState.Open) await websocket.SendText(displayId);
        else Debug.LogError("Server is not ready to connect!");
    }
}