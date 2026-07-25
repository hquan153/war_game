using UnityEngine;
using TMPro;

public class Player : PlayerController
{
    private const int speed = 6;

    private Rigidbody2D rigidbody2d;

    /*private PlayerData m_Player
    {
    }*/

    private TMP_Text healthTMP;

    private int health;
    public int Health
    {
        get => health;
        set
        {
            health = value;
            healthTMP.text = health.ToString();
        }
    }

    private void Awake()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        healthTMP = transform.Find("Health").GetComponent<TMP_Text>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.transform.name);
        if (!collision.transform.CompareTag("DeathZone"))
        {
            return;
        }
        if (!collision.transform.name.Contains("Player") || !collision.transform.name.Contains(splitChar)) return;

        Health -= int.Parse(collision.transform.name.Split(splitChar)[0]);
        healthTMP.text = Health.ToString();

        IsDead();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.GetComponent<Collider2D>().isTrigger = false;
    }

    private void OnEnable()
    {
        rigidbody2d.linearVelocity = new Vector2(-speed, Random.Range(-speed, speed));
    }

    private void IsDead()
    {
        if (Health > 0) return;
        //Debug.Log($"Player {transform.name} is dead!");
        
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        rigidbody2d.linearVelocity = Vector2.zero;
        
        base.PlayerDeadHandler(gameObject);
    }
}
