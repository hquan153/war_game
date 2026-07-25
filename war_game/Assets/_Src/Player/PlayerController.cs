using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject[] playerArray;
    private readonly Queue<GameObject> playersGO = new();
    private readonly Queue<GameObject> players = new();

    private void Awake()
    {
        playerArray = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in playerArray)
        {
            playersGO.Enqueue(player);
            player.SetActive(false);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PlayerCreateHandler(PlayerData player)
    {
        if (playersGO.Count == 0)
        {
            Debug.Log("No available player object in the queue:");
            return;
        }

        GameObject playerGO = playersGO.Dequeue();
        playerGO.SetActive(true);

        Sprite avatarSprite = CreateSpriteFromBuffer(player.avatarBuffer);
        playerGO.GetComponent<Player>().Create(player, avatarSprite);
    }

    private Sprite CreateSpriteFromBuffer(byte[] avatarBuffer)
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

    protected void PlayerDeadHandler(GameObject playerGO)
    {
        playerGO.SetActive(false);
        playersGO.Enqueue(playerGO);

        Debug.Log(playerGO.name + " Dead!");
    }
}
