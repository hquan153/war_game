using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Connection connectionScript;
    private AvatarLoader avatarLoaderScript;

    private void Awake()
    {
        Application.runInBackground = true;
        #if !UNITY_EDITOR
            Screen.SetResolution(540, 960, false); 
        #endif

        connectionScript = transform.GetComponent<Connection>();
        avatarLoaderScript = transform.GetComponent<AvatarLoader>();
    }

    async private void Start()
    {
        connectionScript.websocket.OnOpen += () =>
        {
            Debug.Log("Connection open!");
        };

        connectionScript.websocket.OnError += (e) => Debug.Log("Error! " + e);

        connectionScript.websocket.OnClose += (code) =>
        {
            Debug.Log("Connection closed!");
        };

        connectionScript.websocket.OnMessage += (bytes) =>
        {
            var messageJSON = System.Text.Encoding.UTF8.GetString(bytes);
            ViewerData message = JsonUtility.FromJson<ViewerData>(messageJSON);
            //Debug.Log("Received: " + message.avatarBase64);
            avatarLoaderScript.RenderAvatar(message.avatarBase64);
        };

        await connectionScript.websocket.Connect();
    }
}