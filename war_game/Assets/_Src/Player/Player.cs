using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer avatarSpriteRenderer;

    private void Awake()
    {
        avatarSpriteRenderer = transform.Find("Avatar").GetComponent<SpriteRenderer>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        
    }

    public void RenderAvatar(Sprite avatarSprite)
    {
        Debug.Log("avatarSprite:" + avatarSprite);
        if (avatarSprite != null)
        {
            Debug.Log("Rendered!");
            avatarSpriteRenderer.sprite = avatarSprite;
        }
    }
}
