using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private AvatarLoader avatarLoaderScript;

    private GameObject[] playerArray;
    private readonly Queue<GameObject> players = new();

    private void Awake()
    {
        Application.runInBackground = true;

        avatarLoaderScript = transform.GetComponent<AvatarLoader>();

        playerArray = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in playerArray)
        {
            players.Enqueue(player);
            player.SetActive(false);
        }
    }

    private void Start()
    {
    }

    public void PlayerDataReceiverHandler(PlayerData player)
    {
        if (players.Count == 0)
        {
            Debug.Log("No available player object in the queue:");
            return;
        }

        GameObject playerGO = players.Dequeue();
        playerGO.SetActive(true);

        Sprite avatarSprite = avatarLoaderScript.CreateSpriteFromBuffer(player.avatarBuffer);
        playerGO.GetComponent<Player>().Create(player, avatarSprite);
    }
}