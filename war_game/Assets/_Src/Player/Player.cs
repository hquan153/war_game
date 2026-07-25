using UnityEngine;
using TMPro;

public class Player : PlayerController
{
    private const int speed = 6;

    private Rigidbody2D rigidbody2d;

    private TMP_Text healthTMP;

    private int health;
    public int Health
    {
        get => health;
        set
        {
            health = value;
            transform.Find("Health").GetComponent<TMP_Text>().text = health.ToString();
        }
    }

    private void Awake()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        healthTMP = transform.Find("Health").GetComponent<TMP_Text>();
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
        if (!collision.transform.name.Contains("Player") || !collision.transform.name.Contains(splitChar)) return;

        Health -= int.Parse(collision.transform.name.Split(splitChar)[0]);
        Debug.Log(int.Parse(collision.transform.name.Split(splitChar)[0]));

        healthTMP.text = Health.ToString();

        IsDead();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.GetComponent<Collider2D>().isTrigger = false;
    }

    private void IsDead()
    {
        if (Health > 0) return;
        Debug.Log($"Player {transform.name} is dead!");
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        base.PlayerDeadHandler(gameObject);
    }
}
