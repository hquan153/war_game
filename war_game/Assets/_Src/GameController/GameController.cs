using System;
using NativeWebSocket;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    private const int serverPort = 8080;
    private readonly string serverUrl = $"ws://127.0.0.1:{serverPort}";

    private WebSocket websocket;

    private PlayerController playerControllerScript;

    private GameObject disconnectedUI;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        websocket = new WebSocket(serverUrl);

        playerControllerScript = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerController>();
        disconnectedUI = GameObject.FindGameObjectWithTag("SettingUI");
    }

    private async void Start()
    {
        websocket.OnOpen += () =>
        {
            Time.timeScale = 1f;

            disconnectedUI.SetActive(false);
            Cursor.visible = false;

            Debug.Log("[WS]: Connection open!");
        };

        websocket.OnClose += (code) =>
        {
            Time.timeScale = 0f;

            disconnectedUI.SetActive(true);
            Cursor.visible = true;

            Debug.Log("[WS]: Connection closed!");
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

        websocket.OnError += (e) => Debug.Log("[WS]: Error! " + e);

        await websocket.Connect();
    }

    private void Update()
    {
        websocket.DispatchMessageQueue();

        if (!Keyboard.current.escapeKey.wasPressedThisFrame) return;
        if (websocket.State != WebSocketState.Open) return;

        disconnectedUI.SetActive(!disconnectedUI.activeSelf);
        Cursor.visible = disconnectedUI.activeSelf;
    }

    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }

    public async void Reconnect()
    {
        if (websocket.State != WebSocketState.Open) await websocket.Connect();
    }

    public async void SendToServer(string message)
    {
        if (websocket.State == WebSocketState.Open) await websocket.SendText(message);
        else Debug.LogError("[WS]: Server is not ready to connect in SendToServer!");
    }
}