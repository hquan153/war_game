using System;
using UnityEngine;

public class AvatarLoader : MonoBehaviour
{
    public Sprite CreateSpriteFromBase64(byte[] avatarBase64)
    {
        //if (string.IsNullOrEmpty(avatarBase64)) return null;
        //else Debug.LogError("avatarBase64 is null or empty");
        Debug.Log(avatarBase64.Length);
        if (avatarBase64 == null || avatarBase64.Length == 0)
        {
            Debug.LogError("Image bytes is null or empty!");
            return null;
        }
        try
        {
            //byte[] imageBytes = Convert.FromBase64String(avatarBase64);

            Texture2D texture = new(72, 72, TextureFormat.RGBA32, false);
            bool isLoaded = ImageConversion.LoadImage(texture, avatarBase64);
            if (!isLoaded)
            {
                Debug.LogError("Failed to load texture from byte array!");
                return null;
            }

            Sprite newSprite = Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f),
                75
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
