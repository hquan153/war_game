using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const string playerPrefabPath = "Prefabs/Player";
    protected const string splitChar = "_";

    private const int spawnInterval = 500; // miliseconds
    private bool isSpawning = false;

    private readonly Queue<GameObject> playersGO = new();
    private readonly Queue<PlayerData> players = new();
    public PlayerData Players { set { players.Enqueue(value); } }

    private void Awake()
    {
        foreach (GameObject playerGO in GameObject.FindGameObjectsWithTag("Player"))
        {
            playersGO.Enqueue(playerGO);
            playerGO.SetActive(false);
        }
    }

    private void Update()
    {
        if (isSpawning || players.Count == 0) return;
        GameObject playerGO;
        if (playersGO.Count == 0)
        {
            playerGO = Resources.Load<GameObject>(playerPrefabPath);
            playerGO = Instantiate(playerGO, transform.localPosition, Quaternion.identity, transform);
            playerGO.name = "Player";
        }
        else
        {
            playerGO = playersGO.Dequeue();
            playerGO.SetActive(true);
        }

        PlayerData player = players.Dequeue();
        player.avatarSprite = CreateSpriteFromBuffer(player.avatarBuffer);

        Render(playerGO.transform, player);
        Assign(playerGO.transform, player);

        isSpawning = true;
        Task.Delay(spawnInterval).ContinueWith(_ => isSpawning = false);
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

    private void Render(Transform playerTransform, PlayerData player)
    {
        playerTransform.localScale = new Vector3(player.size, player.size, player.size); // scale

        playerTransform.Find("Avatar").GetComponent<SpriteRenderer>().sprite = player.avatarSprite; // avatar

        if (ColorUtility.TryParseHtmlString(player.borderColor.ToLower(), out Color newBorderColor))
            playerTransform.Find("Border").GetComponent<SpriteRenderer>().color = newBorderColor; // border color
        else Debug.LogWarning($"{player.borderColor} is invalid color string!");

        if (player.tier != "base")
        {
            if (ColorUtility.TryParseHtmlString(player.color.ToLower(), out Color newColor))
                playerTransform.Find("Saw Blade").GetComponent<SpriteRenderer>().color = newColor; // saw blade color
            else Debug.LogWarning($"{player.color} is invalid color string!");
        }
        else playerTransform.Find("Saw Blade").GetComponent<SpriteRenderer>().enabled = false;

        //Debug.Log("Rendered!");
    }

    private void Assign(Transform playerTransform, PlayerData player)
    {
        // name: damge_Player_displayId
        playerTransform.name = $"{player.damage}{splitChar}Player{splitChar}{player.displayId}";
        playerTransform.GetComponent<Player>().Health = player.health;

        Debug.Log(playerTransform.GetComponent<Player>().Health);
    }

    protected void PlayerDeadHandler(GameObject playerGO)
    {
        playerGO.name = "Player";
        playersGO.Enqueue(playerGO);
        playerGO.SetActive(false);

        //Debug.Log($"{playerGO.name} Dead!");
    }
}
