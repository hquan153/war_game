using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameController gameControllerScript;

    private const string playerPrefabPath = "Prefabs/Player";
    public readonly string splitChar = " ";

    private readonly Queue<GameObject> playersGO = new();
    private readonly Queue<PlayerData> m_players = new();
    public PlayerData Players
    {
        get => m_players.Dequeue();
        set { m_players.Enqueue(value); }
    }

    //private int m_playersCount = 0;

    private void Awake()
    {
        gameControllerScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        foreach (GameObject playerGO in GameObject.FindGameObjectsWithTag("Player"))
        {
            playersGO.Enqueue(playerGO);
            playerGO.SetActive(false);
        }
    }

    private void Update()
    {
        //m_playersCount = m_players.Count;
        if (m_players.Count == 0) return;

        PlayerData player = Players;
        GameObject playerGO;

        if (player.diamondCount > 0 || !player.attended)
        {
            if (playersGO.Count > 0)
            {
                playerGO = playersGO.Dequeue();
                playerGO.SetActive(true);
            }
            else
            {
                GameObject newPlayerGO = Resources.Load<GameObject>(playerPrefabPath);
                playerGO = Instantiate(newPlayerGO, transform.localPosition, Quaternion.identity, transform);
                playerGO.name = "Player";
            }
        }
        else return;

        player.avatarSprite = CreateSpriteFromBuffer(player.avatarBuffer);

        Render(playerGO.transform, player);
        Assign(playerGO.transform, player);
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
            playerTransform.Find("Saw Blade").GetComponent<SpriteRenderer>().enabled = true;
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
        Player playerScript = playerTransform.GetComponent<Player>();
        playerScript.Health = player.health;
        playerScript.PlayerF = player;
        playerTransform.GetComponent<Rigidbody2D>().mass = player.mass;
        //Debug.Log(playerTransform.GetComponent<Player>().Health);
    }

    public void PlayerDeadHandler(GameObject playerGO)
    {
        gameControllerScript.RemovePlayer(playerGO.name.Split(splitChar)[2]);

        playersGO.Enqueue(playerGO);
        playerGO.name = "Player";
        playerGO.SetActive(false);
    }
}
