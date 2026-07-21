using System;
using UnityEngine;

public class AvatarLoader : MonoBehaviour
{
    [SerializeField] private SpriteRenderer avatarSpriteRenderer;

    private Sprite CreateSpriteFromBase64(string avatarBase64)
    {
        if (string.IsNullOrEmpty(avatarBase64)) return null;

        try
        {
            byte[] imageBytes = Convert.FromBase64String(avatarBase64);

            Texture2D texture = new(0, 0, TextureFormat.RGBA32, false);
            if (!texture.LoadImage(imageBytes)) return null;

            Sprite newSprite = Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f)
            );

            return newSprite;
        }
        catch (Exception error)
        {
            Debug.LogError($"Error when rendering avatar: {error.Message}");
            return null;
        }
    }

    public void RenderAvatar(string avatarBase64)
    {
        Sprite avatarSprite = CreateSpriteFromBase64(avatarBase64);
        if (avatarSprite != null)
        {
            Debug.Log("Rendered!");
            avatarSpriteRenderer.sprite = avatarSprite;
        }
    }
}
