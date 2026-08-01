using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const float degree = 360;
    private const float speed = 18f;

    private PlayerController playerControllerScript;

    private Rigidbody2D rigidbody2d;
    private TMP_Text healthTMP;

    private PlayerData m_player;
    public PlayerData PlayerF
    {
        get => m_player;
        set { m_player = value; }
    }

    private void Awake()
    {
        playerControllerScript = transform.GetComponentInParent<PlayerController>();

        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        healthTMP = transform.Find("Health").GetComponent<TMP_Text>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Transform collisionTransform = collision.transform;
        if (collisionTransform.CompareTag("DeathZone"))
        {
            IsDead(true);
            playerControllerScript.Players = PlayerF;
            return;
        }
        if (!collisionTransform.CompareTag("Player")) return;

        PlayerF.health -= collisionTransform.GetComponent<Player>().PlayerF.damage;
        healthTMP.text = PlayerF.health.ToString();
        IsDead();
    }

    private void OnEnable()
    {
        rigidbody2d.linearVelocity = (new Vector2(Random.Range(-degree, degree), Random.Range(-degree, degree))).normalized * speed;
    }

    private void OnDestroy()
    {
        IsDead(true);
    }

    private void IsDead(bool isDead = false)
    {
        if (PlayerF.health > 0 && !isDead) return;
        if (!gameObject.activeSelf && !isDead) return;

        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        rigidbody2d.linearVelocity = Vector2.zero;

        PlayerF.health = 0;

        playerControllerScript.PlayerDeadHandler(gameObject);
    }
}
