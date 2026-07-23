using UnityEngine;

public class Player : MonoBehaviour
{
    private int health;
    private int damage;

    private void Awake()
    {
    }

    private void Start()
    {

    }

    private void Update()
    {

    }

    private void Render(Sprite avatarSprite, PlayerData player)
    {
        transform.localScale = new Vector3(player.size, player.size, player.size);

        transform.Find("Avatar").GetComponent<SpriteRenderer>().sprite = avatarSprite;

        if (ColorUtility.TryParseHtmlString(player.borderColor.ToLower(), out Color newBorderColor))
            transform.Find("Border").GetComponent<SpriteRenderer>().color = newBorderColor;
        else Debug.LogWarning("Invalid color string!");

        if (player.tier != "base")
        {
            if (ColorUtility.TryParseHtmlString(player.color.ToLower(), out Color newColor))
                transform.Find("Saw Blade").GetComponent<SpriteRenderer>().color = newColor;
            else Debug.LogWarning("Invalid color string!");
        }
        else transform.Find("Saw Blade").GetComponent<SpriteRenderer>().enabled = false;

        Debug.Log("Rendered!");
    }

    private void Assign(PlayerData player)
    {
        health = player.health;
        damage = player.damage;
    }

    public void Create(PlayerData player, Sprite avatarSprite)
    {
        Render(avatarSprite, player);
        Assign(player);
    }
}
