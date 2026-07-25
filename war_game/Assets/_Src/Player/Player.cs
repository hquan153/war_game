using UnityEngine;
using TMPro;

public class Player : PlayerController
{
    private const int speed = 6;
    private const string splitChar = "_";

    private Rigidbody2D rigidbody2d;

    private TMP_Text healthTMP;

    private int health;
    //private int damage;

    private void Awake()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        healthTMP = transform.Find("Health").GetComponent<TMP_Text>();
    }

    private void Start()
    {
    }

    private void Update()
    {

    }

    private void OnEnable()
    {
        rigidbody2d.linearVelocity = new Vector2(-speed, Random.Range(-speed, speed));
    }

    private void OnDisable()
    {
        rigidbody2d.linearVelocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.transform.name);
        if (!collision.transform.name.Contains("Player")) return;

        int collisionDamage = int.Parse(collision.transform.name.Split(splitChar)[0]);
        health -= collisionDamage;

        healthTMP.text = health.ToString();

        Dead();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.GetComponent<Collider2D>().isTrigger = false;
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
        transform.name = $"{player.damage}{splitChar}Player{splitChar}{player.displayId}";

        health = player.health;
        //damage = player.damage;

        healthTMP.text = health.ToString();
    }

    private void Dead()
    {
        if (health > 0) return;
        //Debug.Log($"Player {transform.name} is dead!");
        transform.localPosition = Vector3.zero;
        base.PlayerDeadHandler(gameObject);
    }

    public void Create(PlayerData player, Sprite avatarSprite)
    {
        Render(avatarSprite, player);
        Assign(player);
    }
}
