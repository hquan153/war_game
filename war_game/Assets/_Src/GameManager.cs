using UnityEngine;

public class GameManager : MonoBehaviour
{
    private AvatarLoader avatarLoaderScript;

    private void Awake()
    {
        Application.runInBackground = true;
#if !UNITY_EDITOR
            Screen.SetResolution(540, 960, false); 
#endif

        avatarLoaderScript = transform.GetComponent<AvatarLoader>();
    }

    private void Start()
    {
    }

    public void ReceiverHandler(ViewerData interactionData)
    {
        avatarLoaderScript.RenderAvatar(interactionData.avatarBase64);

    }
}