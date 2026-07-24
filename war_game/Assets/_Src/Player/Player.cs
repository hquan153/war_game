using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    private const string splitChar = "_";

    private GameController gameControllerScript;

    private int health;
    //private int damage;

    private void Awake()
    {
        gameControllerScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void Start()
    {

    }

    private void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int collisionDamage = int.Parse(collision.transform.name.Split(splitChar)[0]);
        health -= collisionDamage;

        Dead();
    }

    private void Render(Sprite avatarSprite, PlayerData player)
    {
        transform.localScale = new Vector3(player.size, player.size, player.size);

        transform.Find("Avatar").GetComponent<SpriteRenderer>().sprite = avatarSprite;

        if (ColorUtility.TryParseHtmlString(player.borderColor.ToLower(), out Color newBorderColor))
            transform.Find("Border").GetComponent<SpriteRenderer>().color = newBorderColor;
        else Debug.LogWarning($"{player.borderColor} is invalid color string!");

        if (player.tier != "base")
        {
            if (ColorUtility.TryParseHtmlString(player.color.ToLower(), out Color newColor))
                transform.Find("Saw Blade").GetComponent<SpriteRenderer>().color = newColor;
            else Debug.LogWarning($"{player.color} is invalid color string!");
        }
        else transform.Find("Saw Blade").GetComponent<SpriteRenderer>().enabled = false;

        //Debug.Log("Rendered!");
    }

    private void Assign(PlayerData player)
    {
        transform.name = $"{player.damage}${splitChar}Player${splitChar}{player.displayId}";

        health = player.health;
        //damage = player.damage;

        transform.Find("Health").GetComponent<TMP_Text>().text = health.ToString();
    }

    private void Dead()
    {
        if (health > 0) return;
        Debug.Log($"Player {transform.name} is dead!");
        gameControllerScript.PlayerDeadHandler(gameObject);
    }

    public void Create(PlayerData player, Sprite avatarSprite)
    {
        Render(avatarSprite, player);
        Assign(player);
    }
}
