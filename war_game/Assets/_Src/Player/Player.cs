using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    private const float speed = 10f;

    private PlayerController playerControllerScript;

    private Rigidbody2D rigidbody2d;
    private TMP_Text healthTMP;
    private Collider2D playerCollider;

    private PlayerData m_playerData;
    public PlayerData PlayerF
    {
        get => m_playerData;
        set { m_playerData = value; }
    }

    private int m_health;
    public int Health
    {
        get => m_health;
        set
        {
            m_health = value;
            healthTMP.text = m_health.ToString();
        }
    }

    private void Awake()
    {
        playerControllerScript = transform.GetComponentInParent<PlayerController>();

        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        healthTMP = transform.Find("Health").GetComponent<TMP_Text>();
        playerCollider = transform.GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("DeathZone"))
        {
            IsDead(true);
            playerControllerScript.Players = PlayerF;
            return;
        }
        if (!collision.transform.name.Contains("Player") || !collision.transform.name.Contains(playerControllerScript.splitChar)) return;

        Health -= int.Parse(collision.transform.name.Split(playerControllerScript.splitChar)[0]);
        healthTMP.text = Health.ToString();

        IsDead();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("MainCamera")) playerCollider.isTrigger = false;
    }

    private void OnEnable()
    {
        rigidbody2d.linearVelocity = (new Vector2(Random.Range(-10, 10), Random.Range(-10, 10))).normalized * speed;
    }

    private void IsDead(bool isDead = false)
    {
        if (Health > 0 && !isDead) return;

        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        rigidbody2d.linearVelocity = Vector2.zero;

        playerControllerScript.PlayerDeadHandler(gameObject);
    }
}
