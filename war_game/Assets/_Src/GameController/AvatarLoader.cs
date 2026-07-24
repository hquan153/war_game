using System;
using UnityEngine;

public class AvatarLoader : MonoBehaviour
{
    public Sprite CreateSpriteFromBuffer(byte[] avatarBuffer)
    {
        if (avatarBuffer == null || avatarBuffer.Length == 0)
        {
            Debug.LogError("Image bytes is null or empty!");
            return null;
        }
        try
        {
            Texture2D texture = new(72, 72, TextureFormat.RGBA32, false);
            bool isLoaded = ImageConversion.LoadImage(texture, avatarBuffer);
            if (!isLoaded)
            {
                Debug.LogError("Failed to load texture from byte array!");
                return null;
            }

            Sprite newSprite = Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f),
                72
            );

            return newSprite;
        }
        catch (Exception error)
        {
            Debug.LogError($"Error when rendering avatar: {error.Message}");
            return null;
        }
    }
}
